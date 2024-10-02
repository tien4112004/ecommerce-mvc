using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Areas.Admin.Services;

public interface IBrandService
{
    public Task<BrandModel?> GetBrandBySlugAsync(string slug);
    public Task<BrandModel?> GetBrandByIdAsync(int brandId);
    public Task<List<BrandModel>> GetAllBrandsAsync();
    public Task CreateBrandAsync(BrandModel brand);
    public Task UpdateBrandAsync(int brandId, BrandModel brand);
    public Task DeleteBrandAsync(int brandId);
}