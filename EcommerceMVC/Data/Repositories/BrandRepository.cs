using EcommerceMVC.Data.Models;
using EcommerceMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data.Data.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly EcommerceDBContext _context;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="BrandRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public BrandRepository(EcommerceDBContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Gets a brand by its slug.
    /// </summary>
    /// <param name="slug">The brand slug.</param>
    /// <returns>The brand model, or null if not found.</returns>
    public async Task<BrandModel?> GetBrandBySlugAsync(string slug)
    {
        return await _context.Brands.FirstOrDefaultAsync(brand => brand.Slug == slug);
    }
    
    /// <summary>
    /// Gets products by brand ID.
    /// </summary>
    /// <param name="brandId">The brand ID.</param>
    /// <returns>A list of product models.</returns>
    public async Task<List<ProductModel>> GetProductsByBrandIdAsync(int brandId)
    {
        return await _context.Products.Where(product => product.BrandId == brandId).ToListAsync();
    }
}