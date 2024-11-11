using EcommerceMVC.Data.Models;
using EcommerceMVC.Helpers;
using EcommerceMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceMVC.Controllers
{
	[Route("Product")]
	public class ProductController : Controller
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[Route("")]
		[Route("Index")]
		public async Task<IActionResult> Index(int page = 1, int pageSize = 12)
		{
			var paginatedProducts = await _productService.GetPaginatedProductsAsync(page, pageSize);
			return View(paginatedProducts);
		}

		[Route("{productId}")]
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
		
		[HttpPost]
		public async Task<IActionResult> Search(string query, int page = 1, int pageSize = 12)
		{
			var paginatedProducts = await _productService.SearchProductsAsync(query, page, pageSize);
			ViewBag.Query = query;
			return View(paginatedProducts);
		}
	}
}