using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Data.Data.Repositories;

public interface ICategoryRepository
{
    /// <summary>
    /// Gets a category by its slug.
    /// </summary>
    /// <param name="slug">The category slug.</param>
    /// <returns>The category model, or null if not found.</returns>
    Task<CategoryModel?> GetCategoryBySlugAsync(string slug);

    /// <summary>
    /// Gets products by category ID.
    /// </summary>
    /// <param name="categoryId">The category ID.</param>
    /// <returns>A list of product models.</returns>
    Task<List<ProductModel>> GetProductsByCategoryIdAsync(int categoryId);
}