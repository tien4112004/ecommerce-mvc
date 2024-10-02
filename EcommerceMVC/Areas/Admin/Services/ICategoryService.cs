using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Areas.Admin.Services;


public interface ICategoryService
{
    public Task<CategoryModel?> GetCategoryBySlugAsync(string slug);
    public Task<CategoryModel?> GetCategoryByIdAsync(int categoryId);
    public Task<List<CategoryModel>> GetAllCategoriesAsync();
    public Task CreateCategoryAsync(CategoryModel category);
    public Task UpdateCategoryAsync(int categoryId, CategoryModel category);
    public Task DeleteCategoryAsync(int categoryId);
}