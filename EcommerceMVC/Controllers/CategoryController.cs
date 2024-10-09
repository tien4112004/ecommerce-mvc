using EcommerceMVC.Data.Models;
using EcommerceMVC.Data;
using EcommerceMVC.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data.Controllers;

/// <summary>
/// Controller for handling category-related actions.
/// </summary>
[Route("category")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryController"/> class.
    /// </summary>
    /// <param name="categoryService">The category service.</param>
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Displays the category page.
    /// </summary>
    /// <param name="slug">The category slug.</param>
    /// <returns>The category view with its products.</returns>
    [Route("{slug}")]
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
