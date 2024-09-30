using EcommerceMVC.Areas.Admin.Services;
using EcommerceMVC.Data;
using EcommerceMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminProductController : Controller
{
    private readonly IAdminProductService _productService;
    private readonly EcommerceDBContext _context;

    public AdminProductController(IAdminProductService productService, EcommerceDBContext context)
    {
        _productService = productService;
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
    public async Task<IActionResult> Create(ProductModel product)
    {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        ViewBag.Brand = new SelectList(_context.Brands, "Id", "Name", product.BrandId);

        if (ModelState.IsValid)
        {
            var result = await _productService.CreateProductAsync(product);
            if (result)
            {
                TempData["Success"] = "Product added successfully.";
                return RedirectToAction("Index");
            }
            else
            {
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
            return BadRequest(errorMessage);
        }

        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int productId)
    {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
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
    public async Task<IActionResult> Edit(int productId, ProductModel product)
    {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        ViewBag.Brand = new SelectList(_context.Brands, "Id", "Name", product.BrandId);

        if (ModelState.IsValid)
        {
            var result = await _productService.UpdateProductAsync(productId, product);
            if (result)
            {
                TempData["Success"] = "Product information updated successfully.";
                return RedirectToAction("Index");
            }
            else
            {
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
            return BadRequest(errorMessage);
        }

        return View(product);
    }

    public async Task<IActionResult> Delete(int productId)
    {
        var result = await _productService.DeleteProductAsync(productId);
        if (result)
        {
            TempData["Success"] = "Product deleted successfully.";
            return RedirectToAction("Index");
        }
        else
        {
            return NotFound();
        }
    }
}