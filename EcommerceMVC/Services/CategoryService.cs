using EcommerceMVC.Data.Data.Repositories;
using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Data.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<CategoryModel?> GetCategoryBySlugAsync(string slug)
    {     
        return await _categoryRepository.GetCategoryBySlugAsync(slug);
    }
    
    public async Task<List<ProductModel>> GetProductsByCategoryIdAsync(int categoryId)
    {
        return await _categoryRepository.GetProductsByCategoryIdAsync(categoryId);
    }
}