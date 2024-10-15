using System.Net;
using System.Security.Claims;
using EcommerceMVC.Data.Models;
using EcommerceMVC.Data;
using EcommerceMVC.Data.Views.ViewModels;
using EcommerceMVC.Data.Extensions;
using EcommerceMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceMVC.Data.Controllers
{
	public class CartController : Controller
	{
		private readonly EcommerceDbContext _context;
		private readonly ISavedAddressService _addressService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private const string CART_KEY = "Cart";

		public CartController(EcommerceDbContext context, ISavedAddressService addressService, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_addressService = addressService;
			_httpContextAccessor = httpContextAccessor;
		}

		public IActionResult Index()
		{
			var cart = HttpContext.Session.Get<List<CartItem>>(CART_KEY) ?? new List<CartItem>();

			CartViewModel cartVM = new()
			{
				CartItems = cart,
				GrandTotal = cart.Sum(item => item.TotalPrice)
			};

			return View(cartVM);
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

		[HttpGet]
		public async Task<IActionResult> Checkout()
		{
			var userIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userIdString == null)
			{
				TempData["Error"] = "Please login to continue.";
				return Redirect("/Account/Login");
			}

			var userId = Guid.Parse(userIdString);
			var addresses = await _addressService.GetByUserIdAsync(userId);

			var cart = HttpContext.Session.Get<List<CartItem>>(CART_KEY);
			if (cart == null || cart.Count <= 0)
			{
				TempData["Error"] = "Cart is empty.";
				return RedirectToAction("Index");
			}

			var cartVM = new CartViewModel
			{
				CartItems = cart,
				GrandTotal = cart.Sum(item => item.TotalPrice)
			};

			var viewData = new CheckoutViewModel
			{
				Cart = cartVM,
				GrandTotal = cart.Sum(item => item.TotalPrice),
				Addresses = addresses.Select(a => new SelectListItem
				{
					Value = a.Id.ToString(),
					Text = $"{a.FullName} - {a.PhoneNumber} - {a.FullAddress}"
				}).ToList(),
				SelectedAddressId = addresses.FirstOrDefault(a => a.IsDefault)?.Id ?? Guid.Empty,
			};

			return View(viewData);
		}

		[HttpPost]
		public async Task<IActionResult> Checkout(CheckoutViewModel model)
		{
			if (!ModelState.IsValid)
			{
				var userIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
				if (userIdString == null)
				{
					TempData["Error"] = "Please login to continue.";
					return Redirect("/Account/Login");
				}

				var userId = Guid.Parse(userIdString);
				var addresses = await _addressService.GetByUserIdAsync(userId);

				model.Addresses = addresses.Select(a => new SelectListItem
				{
					Value = a.Id.ToString(),
					Text = $"{a.FullName} - {a.PhoneNumber} - {a.FullAddress}"
				}).ToList();

				return View(model);
			}

			// Process the order using model.SelectedAddressId and model.NewAddress
			// Example: var selectedAddress = _addressService.GetAddressById(model.SelectedAddressId);

			// Redirect to a confirmation page or another action
			return RedirectToAction("Index");
		}
	}
}
