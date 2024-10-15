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

    public async Task<IActionResult> Detail(Guid orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        return View(order);
    }
    
    public async Task<IActionResult> CancelOrder(Guid orderId)
    {
        await _orderService.UpdateOrderStatusAsync(orderId, OrderStatus.CANCELLED);
        return RedirectToAction(nameof(Detail), new { orderId });
    }
}
