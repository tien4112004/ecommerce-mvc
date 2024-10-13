using System.Security.Claims;
using EcommerceMVC.Data.Extensions;
using EcommerceMVC.Data.Models;
using EcommerceMVC.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderController(IOrderService orderService, IHttpContextAccessor httpContextAccessor)
    {
        _orderService = orderService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IActionResult> Checkout()
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var cart = HttpContext.Session.Get<List<CartItem>>("Cart");

        if (cart == null || !cart.Any())
        {
            return RedirectToAction("Index", "Cart");
        }

        try
        {
            await _orderService.CreateOrderAsync(Guid.Parse(userId), cart);
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
}
