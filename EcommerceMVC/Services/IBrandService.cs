using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Data.Services;

public interface IBrandService
{
    Task<BrandModel?> GetBrandBySlugAsync(string slug);
    Task<List<ProductModel>> GetProductsByBrandIdAsync(int brandId);
}