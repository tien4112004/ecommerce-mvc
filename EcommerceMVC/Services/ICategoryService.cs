using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Data.Services;

public interface ICategoryService
{
    Task<CategoryModel?> GetCategoryBySlugAsync(string slug);
    Task<List<ProductModel>> GetProductsByCategoryIdAsync(int categoryId);
}