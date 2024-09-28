﻿using EcommerceMVC.Models;
using EcommerceMVC.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Controllers
{

	public class CategoryController : Controller
	{
		private readonly EcommerceDBContext _context;

		public CategoryController(EcommerceDBContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index(string slug = "")
		{
			CategoryModel? category = await _context.Categories.FirstOrDefaultAsync(category => category.Slug == slug);
			if (category == null)
			{
				return RedirectToAction("Index");
			}

			var products = await _context.Products.Where(product => product.CategoryId == category.Id).ToListAsync();
			return View(products);
		}
	}
}
