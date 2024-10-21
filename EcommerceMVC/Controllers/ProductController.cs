using EcommerceMVC.Data;
using EcommerceMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data.Controllers
{
	public class ProductController : Controller
	{
		private readonly EcommerceDbContext _context;

		public ProductController(EcommerceDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var products = _context.Products.Where(p => p.Status != ProductStatus.Inactive);
			return View(products);
		}

		public async Task<IActionResult> Detail(int? productId)
		{
			if (productId == null) return Redirect("Index");
			var product = await _context.Products.Where(product => product.Id == productId).FirstOrDefaultAsync();
			if (product == null)
			{
				return Redirect("/404");
			}
			return View(product);
		}
	}
}
