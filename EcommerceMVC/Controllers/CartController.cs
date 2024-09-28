using EcommerceMVC.Models;
using EcommerceMVC.Repository;
using EcommerceMVC.Repository.Extensions;
using EcommerceMVC.Views.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers
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
			var cart = HttpContext.Session.Get<List<CartItemModel>>(CART_KEY) ?? new List<CartItemModel>();

			CartItemViewModel cartVM = new()
			{
				CartItems = cart,
				GrandTotal = cart.Sum(item => item.TotalPrice)
			};

			return View(cartVM);
		}

		public IActionResult Checkout()
		{
			return View("~/Views/Checkout/Index.cshtml");
		}

		public IActionResult DecreaseQuantity()
		{
			throw new NotImplementedException();
		}

		public IActionResult IncreaseQuantity()
		{
			throw new NotImplementedException();
		}

		public IActionResult RemoveFromCart()
		{
			throw new NotImplementedException();
		}
	}
}
