using System.Security.Claims;
using AspNetCoreGeneratedDocument;
using EcommerceMVC.Data.Extensions;
using EcommerceMVC.Data.Models;
using EcommerceMVC.Data.Views.ViewModels;
using EcommerceMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers;

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
            await _orderService.CreateOrderAsync(Guid.Parse(userId), cart, address, model.Note);
        }
        catch (Exception e)
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

        var orders = _orderService.GetOrdersByUser(Guid.Parse(userId));

        return View(orders);
    }

    public async Task<IActionResult> Detail(Guid orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        if (order == null)
        {
            throw new Exception("Order not found");
            return Redirect("/404");
        }

        return View(order);
    }

    public async Task<IActionResult> CancelOrder(Guid orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        if (order == null)
        {
            throw new Exception("Order not found");
            return Redirect("/404");
        }

        await _orderService.CancelOrderAsync(orderId);
        TempData["Success"] = "Order cancelled successfully.";
        return RedirectToAction("Index");
    }
}
