using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Services;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
}