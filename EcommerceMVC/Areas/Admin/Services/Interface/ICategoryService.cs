using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Areas.Admin.Services;


public interface ICategoryService
{
    public Task<Category?> GetCategoryBySlugAsync(string slug);
    public Task<Category?> GetCategoryByIdAsync(int categoryId);
    public Task<List<Category>> GetAllCategoriesAsync();
    public Task CreateCategoryAsync(Category category);
    public Task UpdateCategoryAsync(int categoryId, Category category);
    public Task DeleteCategoryAsync(int categoryId);
}