using EcommerceMVC.Data.Models;
using EcommerceMVC.Data.Services;
using EcommerceMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data.Controllers;

/// <summary>
/// Controller for handling brand-related actions.
/// </summary>
[Route("brand")]
public class BrandController : Controller
{
	private readonly IBrandService _brandService;

	/// <summary>
	/// Initializes a new instance of the <see cref="BrandController"/> class.
	/// </summary>
	/// <param name="service">The brand service.</param>
	public BrandController(IBrandService service)
	{
		_brandService = service;
	}

	/// <summary>
	/// Displays the brand page.
	/// </summary>
	/// <param name="slug">The brand slug.</param>
	/// <returns>The brand view with its products.</returns>
	[Route("{slug}")]
	public async Task<IActionResult> Index(string slug = "")
	{
		Brand? brand = await _brandService.GetBrandBySlugAsync(slug);
		if (brand == null)
		{
			return RedirectToAction("Index");
		}

		var products = await _brandService.GetProductsByBrandIdAsync(brand.Id);
		return View(products);
	}
}
