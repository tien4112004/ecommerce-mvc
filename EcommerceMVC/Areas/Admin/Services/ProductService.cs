using EcommerceMVC.Data.Models;
using EcommerceMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceMVC.Areas.Admin.Services;

public class ProductService : IProductService
{
    private readonly EcommerceDBContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ProductService(EcommerceDBContext context, IWebHostEnvironment hostEnvironment)
    {
        _context = context;
        _hostEnvironment = hostEnvironment;
    }

    public async Task<List<ProductModel>> GetAllProductsAsync()
    {
        return await _context.Products.OrderByDescending(product => product.Id)
                                      .Include(product => product.Category)
                                      .Include(product => product.Brand)
                                      .ToListAsync();
    }

    public async Task<ProductModel?> GetProductByIdAsync(int productId)
    {
        return await _context.Products.FindAsync(productId);
    }

    public async Task<List<ProductModel>> GetProductsByCategoryIdAsync(int categoryId)
    {
        return await _context.Products.Where(p => p.CategoryId == categoryId)
                                      .Include(p => p.Category)
                                      .Include(p => p.Brand)
                                      .ToListAsync();
    }

    public async Task CreateProductAsync(ProductModel product)
    {
        product.Slug = product.Name.Replace(" ", "-");
        var slug = await _context.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
        if (slug != null)
        {
            throw new InvalidOperationException("A product with this slug already exists.");
        }
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
    }

    public async Task UpdateProductAsync(int productId, ProductModel product)
    {
        var existedProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (existedProduct == null)
        {
            throw new InvalidOperationException("Product not found.");
        }
        product.Slug = product.Name.Replace(" ", "-");
        var slug = await _context.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
        if (slug != null && slug.Id != productId)
        {
            throw new InvalidOperationException("A product with this slug already exists.");
        }
        if (product.ImageUpload != null)
        {
            string uploadDir = Path.Combine(_hostEnvironment.WebRootPath, "media/products");
        
            if (existedProduct.Image != null)
            {
                string existingImagePath = Path.Combine(uploadDir, existedProduct.Image);
                if (System.IO.File.Exists(existingImagePath))
                {
                    System.IO.File.Delete(existingImagePath);
                }
            }
        
            string imageName = Guid.NewGuid().ToString() + product.ImageUpload.FileName;
            string newImagePath = Path.Combine(uploadDir, imageName);
        
            using (FileStream fs = new FileStream(newImagePath, FileMode.Create))
            {
                await product.ImageUpload.CopyToAsync(fs);
            }
        
            existedProduct.Image = imageName;
        }
        existedProduct.Name = product.Name;
        existedProduct.Description = product.Description;
        existedProduct.Price = product.Price;
        existedProduct.CategoryId = product.CategoryId;
        existedProduct.BrandId = product.BrandId;
        existedProduct.Slug = product.Slug;
        _context.Products.Update(existedProduct);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            throw new InvalidOperationException("Product not found.");
        }
        if (product.Image != null)
        {
            string uploadDir = Path.Combine(_hostEnvironment.WebRootPath, "media/products");
            string filePath = Path.Combine(uploadDir, product.Image);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<List<CategoryModel>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<List<BrandModel>> GetBrandsAsync()
    {
        return await _context.Brands.ToListAsync();
    }
}