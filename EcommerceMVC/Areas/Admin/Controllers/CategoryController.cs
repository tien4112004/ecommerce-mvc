using EcommerceMVC.Areas.Admin.Services;
using EcommerceMVC.Data;
using EcommerceMVC.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EcommerceMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
[Authorize(Roles = UserRoles.Administrator)]
public class CategoryController : Controller {
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly EcommerceDBContext _context;

    public CategoryController(IProductService productService, ICategoryService categoryService, EcommerceDBContext context)
    {
        _productService = productService;
        _categoryService = categoryService;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _categoryService.CreateCategoryAsync(category);
                TempData["Success"] = "Category added successfully.";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException)
            {
                TempData["Error"] = "A category with this slug already exists.";
                ModelState.AddModelError("Slug", "A category with this slug already exists.");
            }
        }
        else
        {
            TempData["Error"] = "Data is invalid. Please check again.";
            List<string> errors = new List<string>();
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }

            string errorMessage = string.Join("\n", errors);
            // return BadRequest(errorMessage);
        }

        return View(category);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int categoryId)
    {
        var category = await _categoryService.GetCategoryByIdAsync(categoryId);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int categoryId, Category category)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _categoryService.UpdateCategoryAsync(categoryId, category);
                TempData["Success"] = "Category information updated successfully.";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
                ModelState.AddModelError("Slug", ex.Message);
            }
        }
        else
        {
            TempData["Error"] = "Data is invalid. Please check again.";
            List<string> errors = new List<string>();
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }

            string errorMessage = string.Join("\n", errors);
            // return BadRequest(errorMessage);
        }

        return View(category);
    }

    public async Task<IActionResult> Delete(int categoryId)
    {
        try
        {
            await _categoryService.DeleteCategoryAsync(categoryId);
            TempData["Success"] = "Category deleted successfully.";
            return RedirectToAction("Index");
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
            return NotFound();
        }
    }
}