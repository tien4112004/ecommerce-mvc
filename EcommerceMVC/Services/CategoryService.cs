using EcommerceMVC.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data.Services;

public class CategoryService : ICategoryService
{
    private readonly EcommerceDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CategoryService(EcommerceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets a category by its slug.
    /// </summary>
    /// <param name="slug">The category slug.</param>
    /// <returns>The category model, or null if not found.</returns>
    public async Task<Category?> GetCategoryBySlugAsync(string slug)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Slug == slug);
    }

    /// <summary>
    /// Gets products by category ID.
    /// </summary>
    /// <param name="categoryId">The category ID.</param>
    /// <returns>A list of product models.</returns>
    public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
    {
        return await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
    }
}