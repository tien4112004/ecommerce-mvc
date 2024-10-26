using EcommerceMVC.Areas.Admin.Services;
using EcommerceMVC.Data;
using EcommerceMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		public async Task<IActionResult> Index()
		{
			var products = await _productService.GetAllProductsAsync();
			return View(products);
		}

		public async Task<IActionResult> Detail(int? productId)
		{
			if (productId == null) return RedirectToAction("Index");
			var product = await _productService.GetProductByIdAsync(productId.Value);
			if (product == null)
			{
				return Redirect("/404");
			}
			return View(product);
		}
	}
}
