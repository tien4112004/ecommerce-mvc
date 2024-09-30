using EcommerceMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly EcommerceDBContext _context;
    
    public CategoryRepository(EcommerceDBContext context)
    {
        _context = context;
    }
    
    public async Task<CategoryModel?> GetCategoryBySlugAsync(string slug)
    {
        return await _context.Categories.FirstOrDefaultAsync(category => category.Slug == slug);
    }
    
    public async Task<List<ProductModel>> GetProductsByCategoryIdAsync(int categoryId)
    {
        return await _context.Products.Where(product => product.CategoryId == categoryId).ToListAsync();
    }
}