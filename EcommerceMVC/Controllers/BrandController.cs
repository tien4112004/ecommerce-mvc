using EcommerceMVC.Models;
using EcommerceMVC.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Controllers
{
	public class BrandController : Controller
	{
		private readonly EcommerceDBContext _context;

		public BrandController(EcommerceDBContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index(string slug = "")
		{
			BrandModel? brand = await _context.Brands.FirstOrDefaultAsync(brand => brand.Slug == slug);
			if (brand == null)
			{
				return RedirectToAction("Index");
			}

			var products = await _context.Products.Where(product => product.BrandId == brand.Id).ToListAsync();
			return View(products);
		}
	}
}
