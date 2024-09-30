using EcommerceMVC.Data.Data.Repositories;
using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Data.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryService"/> class.
    /// </summary>
    /// <param name="categoryRepository">The category repository.</param>
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    /// <summary>
    /// Gets a category by its slug.
    /// </summary>
    /// <param name="slug">The category slug.</param>
    /// <returns>The category model, or null if not found.</returns>
    public async Task<CategoryModel?> GetCategoryBySlugAsync(string slug)
    {
        return await _categoryRepository.GetCategoryBySlugAsync(slug);
    }

    /// <summary>
    /// Gets products by category ID.
    /// </summary>
    /// <param name="categoryId">The category ID.</param>
    /// <returns>A list of product models.</returns>
    public async Task<List<ProductModel>> GetProductsByCategoryIdAsync(int categoryId)
    {
        return await _categoryRepository.GetProductsByCategoryIdAsync(categoryId);
    }
}