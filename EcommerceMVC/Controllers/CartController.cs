using System.Net;
using EcommerceMVC.Data.Models;
using EcommerceMVC.Data;
using EcommerceMVC.Data.Views.ViewModels;
using EcommerceMVC.Data.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Data.Controllers
{
	public class CartController : Controller
	{
		private readonly EcommerceDBContext _context;
		private const string CART_KEY = "Cart";

		public CartController(EcommerceDBContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var cart = HttpContext.Session.Get<List<CartItem>>(CART_KEY) ?? new List<CartItem>();

			CartItemViewModel cartVM = new()
			{
				CartItems = cart,
				GrandTotal = cart.Sum(item => item.TotalPrice)
			};

			return View(cartVM);
		}

		public IActionResult Checkout(Guid userId)
		{	
			return View("~/Views/Checkout/Index.cshtml");
		}

		public IActionResult AddToCart(int productId, int quantity = 1)
		{
			var product = _context.Products.Find(productId);
			if (product == null)
			{
				return Redirect("/404");
			}

			var cart = HttpContext.Session.Get<List<CartItem>>(CART_KEY) ?? new List<CartItem>();

			var cartItem = cart.FirstOrDefault(item => item.ProductId == productId);
			if (cartItem == null)
			{
				cart.Add(new CartItem
				{
					ProductId = productId,
					ProductName = product.Name,
					Quantity = quantity,
					UnitPrice = product.Price
				});
			}
			else
			{
				cartItem.Quantity += quantity;
			}

			HttpContext.Session.Set(CART_KEY, cart);
			TempData["Success"] = "Added to cart successfully.";
			return Redirect(Request.Headers["Referer"].ToString());
		}
		
		public IActionResult DecreaseQuantity(int productId)
		{
			var cart = HttpContext.Session.Get<List<CartItem>>(CART_KEY);
			var cartItem = cart.FirstOrDefault(item => item.ProductId == productId);
			cartItem.Quantity -= 1;

			if (cartItem.Quantity <= 0)
			{
				return RemoveFromCart(productId);
			}
			
			HttpContext.Session.Set(CART_KEY, cart);

			return RedirectToAction("Index");		
		}

		public IActionResult IncreaseQuantity(int productId)
		{
			var cart = HttpContext.Session.Get<List<CartItem>>(CART_KEY);
			var cartItem = cart.FirstOrDefault(item => item.ProductId == productId);
			cartItem.Quantity += 1;
			
			HttpContext.Session.Set(CART_KEY, cart);

			return RedirectToAction("Index");		
		}

		public IActionResult RemoveFromCart(int productId)
		{
			var cart = HttpContext.Session.Get<List<CartItem>>(CART_KEY);
			var cartItem = cart.FirstOrDefault(item => item.ProductId == productId);
			cart.Remove(cartItem);

			if (cart.Count() <= 0)
			{
				return ClearCart();
			} 
			
			HttpContext.Session.Set(CART_KEY, cart);
			return RedirectToAction("Index");			
		}
		
		public IActionResult ClearCart()
		{
			HttpContext.Session.Remove(CART_KEY);
			TempData["Success"] = "Cleared cart.";
			return RedirectToAction("Index");
		}
	}
}
