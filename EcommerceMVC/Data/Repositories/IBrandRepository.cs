using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Data.Data.Repositories;

public interface IBrandRepository
{
    /// <summary>
    /// Gets a brand by its slug.
    /// </summary>
    /// <param name="slug">The brand slug.</param>
    /// <returns>The brand model, or null if not found.</returns>
    Task<BrandModel?> GetBrandBySlugAsync(string slug);  

    /// <summary>
    /// Gets products by brand ID.
    /// </summary>
    /// <param name="brandId">The brand ID.</param>
    /// <returns>A list of product models.</returns>
    Task<List<ProductModel>> GetProductsByBrandIdAsync(int brandId);
}

