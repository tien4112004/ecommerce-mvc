using EcommerceMVC.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceMVC.Areas.Admin.Services;

public interface IAdminProductService
{
    Task<List<ProductModel>> GetAllProductsAsync();
    Task<ProductModel?> GetProductByIdAsync(int productId);
    Task<bool> CreateProductAsync(ProductModel product);
    Task<bool> UpdateProductAsync(int productId, ProductModel product);
    Task<bool> DeleteProductAsync(int productId);
}