using EcommerceMVC.Data.Models;
using EcommerceMVC.Data;
using EcommerceMVC.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data.Controllers
{

	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoryService;
		
		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		public async Task<IActionResult> Index(string slug = "")
		{
			var category = await _categoryService.GetCategoryBySlugAsync(slug);
			if (category == null)
			{
				return NotFound();
			}
			var products = await _categoryService.GetProductsByCategoryIdAsync(category.Id);
			return View(products);
		}
	}
}
