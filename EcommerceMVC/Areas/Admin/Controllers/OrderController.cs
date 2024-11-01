using System;
using EcommerceMVC.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EcommerceMVC.Areas.Admin.Services;

namespace EcommerceMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = UserRoles.Administrator)]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return View(orders);
    }

    public async Task<IActionResult> Detail(string orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        return View(order);
    }

    public async Task<IActionResult> CancelOrder(string orderId)
    {
        await _orderService.UpdateOrderStatusAsync(orderId, OrderStatus.CANCELLED);
        return RedirectToAction(nameof(Detail), new { area = "Admin", orderId });
    }
    
    public async Task<IActionResult> ConfirmOrder(string orderId)
    {
        await _orderService.UpdateOrderStatusAsync(orderId, OrderStatus.CONFIRMED);
        return RedirectToAction(nameof(Detail), new { area = "Admin", orderId });
    }
    
    public async Task<IActionResult> PrepareOrder(string orderId)
    {
        await _orderService.UpdateOrderStatusAsync(orderId, OrderStatus.PREPARED);
        return RedirectToAction(nameof(Detail), new { area = "Admin", orderId });
    }
}
