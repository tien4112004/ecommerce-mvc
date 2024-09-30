using EcommerceMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly EcommerceDBContext _context;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CategoryRepository(EcommerceDBContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Gets a category by its slug.
    /// </summary>
    /// <param name="slug">The category slug.</param>
    /// <returns>The category model, or null if not found.</returns>
    public async Task<CategoryModel?> GetCategoryBySlugAsync(string slug)
    {
        return await _context.Categories.FirstOrDefaultAsync(category => category.Slug == slug);
    }
    
    /// <summary>
    /// Gets products by category ID.
    /// </summary>
    /// <param name="categoryId">The category ID.</param>
    /// <returns>A list of product models.</returns>
    public async Task<List<ProductModel>> GetProductsByCategoryIdAsync(int categoryId)
    {
        return await _context.Products.Where(product => product.CategoryId == categoryId).ToListAsync();
    }
}