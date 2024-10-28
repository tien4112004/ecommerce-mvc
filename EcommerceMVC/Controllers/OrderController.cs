using System.Security.Claims;
using EcommerceMVC.Helpers.Extensions;
using EcommerceMVC.Data.Models;
using EcommerceMVC.Data.Views.ViewModels;
using EcommerceMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers;

[Authorize]
[Route("Account/[Controller]/[Action]")]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly ISavedAddressService _addressService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderController(IOrderService orderService, ISavedAddressService addressService, IHttpContextAccessor httpContextAccessor)
    {
        _orderService = orderService;
        _addressService = addressService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CheckoutViewModel model)
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var cart = HttpContext.Session.Get<List<CartItem>>("Cart");

        if (cart == null || !cart.Any())
        {
            TempData["Error"] = "No item found in cart!";
            return RedirectToAction("Index", "Cart");
        }

        var address = await _addressService.GetByIdAsync(model.SelectedAddressId);

        try
        {
            await _orderService.CreateOrderAsync(userId, cart, address, model.Note);
        }
        catch (Exception)
        {
            return Redirect("/404");
        }

        cart.Clear();
        HttpContext.Session.Set("Cart", cart);

        TempData["Success"] = "Order created successfully.";
        return RedirectToAction("Index", "Cart");
    }

    public async Task<IActionResult> Index()
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var orders = _orderService.GetOrdersByUser(userId);

        return View(orders);
    }

    public async Task<IActionResult> Detail(string orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        if (order == null)
        {
            // throw new Exception("Order not found");
            return Redirect("/404");
        }

        return View(order);
    }

    public async Task<IActionResult> CancelOrder(string orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        if (order == null)
        {
            // throw new Exception("Order not found");
            return Redirect("/404");
        }

        await _orderService.CancelOrderAsync(orderId);
        TempData["Success"] = "Order cancelled successfully.";
        return RedirectToAction("Index");
    }
}
