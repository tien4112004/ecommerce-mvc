using EcommerceMVC.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceMVC.Areas.Admin.Services;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
    Task CreateProductAsync(Product product);
    Task UpdateProductAsync(int productId, Product product);
    Task DeleteProductAsync(int productId);
}