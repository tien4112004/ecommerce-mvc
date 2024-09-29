using EcommerceMVC.Models;
using Microsoft.AspNetCore.Mvc;
using EcommerceMVC.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Area.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly EcommerceDBContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ProductController(EcommerceDBContext context, IWebHostEnvironment hostEnvironment)
    {
        _context = context;
        _hostEnvironment = hostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Products.OrderByDescending(product => product.Id)
                                        .Include(product => product.Category)
                                        .Include(product => product.Brand)
                                        .ToListAsync()
            );
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        ViewBag.Brand = new SelectList(_context.Brands, "Id", "Name");
        
        return View();
    }

    public async Task<IActionResult> Create(ProductModel product)
    {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        ViewBag.Brand = new SelectList(_context.Brands, "Id", "Name", product.BrandId);

        if (ModelState.IsValid)
        {
            product.Slug = product.Name.Replace(" ", "-");
            var slug = await _context.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
            if (slug != null)
            {
                ModelState.AddModelError("Slug", "A product with this slug already exists.");
                return View(product);
            }
            else
            {
                if (product.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(_hostEnvironment.WebRootPath, "media/products");
                    string imageName = Guid.NewGuid().ToString() + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);

                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageUpload.CopyToAsync(fs);
                    }
                    product.Image = imageName;
                }
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Product added successfully.";
                return RedirectToAction("Index");
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
    public IActionResult Edit()
    {
        throw new NotImplementedException();
    }

    public IActionResult Delete()
    {
        throw new NotImplementedException();
    }
}