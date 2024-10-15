using EcommerceMVC.Areas.Admin.Services;
using EcommerceMVC.Data;
using EcommerceMVC.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = UserRoles.Administrator)]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly EcommerceDbContext _context;

    public ProductController(IProductService productService, ICategoryService categoryService, EcommerceDbContext context)
    {
        _productService = productService;
        _categoryService = categoryService;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProductsAsync();
        return View(products);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        ViewBag.Brand = new SelectList(_context.Brands, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        ViewBag.Brand = new SelectList(_context.Brands, "Id", "Name", product.BrandId);

        if (ModelState.IsValid)
        {
            try
            {
                await _productService.CreateProductAsync(product);
                TempData["Success"] = "Product added successfully.";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException)
            {
                TempData["Error"] = "A product with this slug already exists.";
                ModelState.AddModelError("Slug", "A product with this slug already exists.");
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

        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int productId)
    {
        ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name");
        ViewBag.Brand = new SelectList(_context.Brands, "Id", "Name");

        var product = await _productService.GetProductByIdAsync(productId);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int productId, Product product)
    {
        ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name", product.CategoryId);
        ViewBag.Brand = new SelectList(_context.Brands, "Id", "Name", product.BrandId);

        if (ModelState.IsValid)
        {
            try
            {
                await _productService.UpdateProductAsync(productId, product);
                TempData["Success"] = "Product information updated successfully.";
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

        return View(product);
    }

    public async Task<IActionResult> Delete(int productId)
    {
        try
        {
            await _productService.DeleteProductAsync(productId);
            TempData["Success"] = "Product deleted successfully.";
            return RedirectToAction("Index");
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
            return NotFound();
        }
    }
}