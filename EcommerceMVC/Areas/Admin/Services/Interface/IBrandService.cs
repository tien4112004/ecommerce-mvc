using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Areas.Admin.Services;

public interface IBrandService
{
    public Task<Brand?> GetBrandBySlugAsync(string slug);
    public Task<Brand?> GetBrandByIdAsync(int brandId);
    public Task<List<Brand>> GetAllBrandsAsync();
    public Task CreateBrandAsync(Brand brand);
    public Task UpdateBrandAsync(int brandId, Brand brand);
    public Task DeleteBrandAsync(int brandId);
}