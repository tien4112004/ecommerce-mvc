using EcommerceMVC.Data.Models;
using EcommerceMVC.Data.Data.Repositories;
using EcommerceMVC.Data.Services;
using EcommerceMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data.Controllers
{
	public class BrandController : Controller
	{
		private readonly IBrandService _brandService;

		public BrandController(IBrandService service)
		{
			_brandService = service;
		}

		public async Task<IActionResult> Index(string slug = "")
		{
			BrandModel? brand = await _brandService.GetBrandBySlugAsync(slug);
			if (brand == null)
			{
				return RedirectToAction("Index");
			}

			var products = await _brandService.GetProductsByBrandIdAsync(brand.Id);
			return View(products);
		}
	}
}
