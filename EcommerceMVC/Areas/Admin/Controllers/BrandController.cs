using EcommerceMVC.Areas.Admin.Services;
using EcommerceMVC.Data;
using EcommerceMVC.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EcommerceMVC.Areas.Admin.Controllers;

[Area("Admin")]
// [Authorize]
[Authorize(Roles = UserRoles.Administrator)]
public class BrandController : Controller {
    private readonly IProductService _productService;
    private readonly IBrandService _brandService;
    private readonly EcommerceDBContext _context;

    public BrandController(IProductService productService, IBrandService brandService, EcommerceDBContext context)
    {
        _productService = productService;
        _brandService = brandService;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _brandService.GetAllBrandsAsync();
        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BrandModel brand)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _brandService.CreateBrandAsync(brand);
                TempData["Success"] = "Brand added successfully.";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException)
            {
                TempData["Error"] = "A brand with this slug already exists.";
                ModelState.AddModelError("Slug", "A brand with this slug already exists.");
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

        return View(brand);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int brandId)
    {
        var brand = await _brandService.GetBrandByIdAsync(brandId);
        if (brand == null)
        {
            return NotFound();
        }
        return View(brand);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int brandId, BrandModel brand)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _brandService.UpdateBrandAsync(brandId, brand);
                TempData["Success"] = "Brand information updated successfully.";
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

        return View(brand);
    }

    public async Task<IActionResult> Delete(int brandId)
    {
        try
        {
            await _brandService.DeleteBrandAsync(brandId);
            TempData["Success"] = "Brand deleted successfully.";
            return RedirectToAction("Index");
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
            return NotFound();
        }
    }
}