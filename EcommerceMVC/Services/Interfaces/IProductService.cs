using EcommerceMVC.Data.Models;
using EcommerceMVC.Helpers;

namespace EcommerceMVC.Services;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<PagedResult<Product>> SearchProductsAsync(string query, int page, int pageSize);
    Task<Product?> GetProductByIdAsync(int productId);
    Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
    Task<PagedResult<Product>> GetPaginatedProductsAsync(int page, int pageSize);
}