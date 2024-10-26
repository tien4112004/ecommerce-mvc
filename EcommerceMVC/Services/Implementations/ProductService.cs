using EcommerceMVC.Areas.Admin.Services;
using EcommerceMVC.Data;
using EcommerceMVC.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Services.Implementations;

public class ProductService : IProductService
{
    private readonly EcommerceDbContext _context;

    public ProductService(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products
                        .Where(p => p.Status != ProductStatus.Inactive)
                        .ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
    {
        return await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
    }
}