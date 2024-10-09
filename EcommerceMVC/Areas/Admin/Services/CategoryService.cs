using EcommerceMVC.Data;
using EcommerceMVC.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Areas.Admin.Services;

public class CategoryService : ICategoryService
{
    private readonly EcommerceDBContext _context;
    
    public CategoryService(EcommerceDBContext context)
    {
        _context = context;
    }
    
    public async Task<Category?> GetCategoryBySlugAsync(string slug)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Slug == slug);
    }
    
    public async Task<Category?> GetCategoryByIdAsync(int categoryId)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
    }
        
    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }
        
    public async Task CreateCategoryAsync(Category category)
    {
        category.Slug = category.Name.Replace(" ", "-");
        var slug = await _context.Categories.FirstOrDefaultAsync(c => c.Slug == category.Slug);
        if (slug != null)
        {
            throw new InvalidOperationException("A category with this slug already exists.");
        }

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateCategoryAsync(int categoryId, Category category)
    {
        var existingCategory = await _context.Categories.FindAsync(categoryId);
        if (existingCategory == null)
        {
            throw new InvalidOperationException("Category not found.");
        }
    
        existingCategory.Name = category.Name;
        existingCategory.Description = category.Description;
        existingCategory.Slug = category.Name.Replace(" ", "-");
        existingCategory.Status = category.Status;
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteCategoryAsync(int categoryId)
    {
        var category = await _context.Categories.FindAsync(categoryId);
        if (category == null)
        {
            throw new InvalidOperationException("Category not found.");
        }
    
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}