using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Data.Services;

public class BrandService : IBrandService
{
    private readonly EcommerceDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="BrandService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public BrandService(EcommerceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets a brand by its slug.
    /// </summary>
    /// <param name="slug">The brand slug.</param>
    /// <returns>The brand model, or null if not found.</returns>
    public async Task<Brand?> GetBrandBySlugAsync(string slug)
    {
        return await _context.Brands.FirstOrDefaultAsync(b => b.Slug == slug);
    }

    /// <summary>
    /// Gets products by brand ID.
    /// </summary>
    /// <param name="brandId">The brand ID.</param>
    /// <returns>A list of product models.</returns>
    public async Task<List<Product>> GetProductsByBrandIdAsync(int brandId)
    {
        return await _context.Products.Where(p => p.BrandId == brandId).ToListAsync();
    }
}