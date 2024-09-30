using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Data.Data.Repositories;

public interface ICategoryRepository
{
    Task<CategoryModel?> GetCategoryBySlugAsync(string slug);
    Task<List<ProductModel>> GetProductsByCategoryIdAsync(int categoryId);
}