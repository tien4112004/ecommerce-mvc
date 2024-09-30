using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceMVC.Data.Data.Repositories;
using EcommerceMVC.Data.Models;
using EcommerceMVC.Data;

namespace EcommerceMVC.Data.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="BrandService"/> class.
    /// </summary>
    /// <param name="brandRepository">The brand repository.</param>
    /// <param name="context">The database context.</param>
    public BrandService(IBrandRepository brandRepository, EcommerceDBContext context)
    {
        _brandRepository = brandRepository;
    }

    /// <summary>
    /// Gets a brand by its slug.
    /// </summary>
    /// <param name="slug">The brand slug.</param>
    /// <returns>The brand model, or null if not found.</returns>
    public async Task<BrandModel?> GetBrandBySlugAsync(string slug)
    {
        return await _brandRepository.GetBrandBySlugAsync(slug);
    }

    /// <summary>
    /// Gets products by brand ID.
    /// </summary>
    /// <param name="brandId">The brand ID.</param>
    /// <returns>A list of product models.</returns>
    public async Task<List<ProductModel>> GetProductsByBrandIdAsync(int brandId)
    {
        return await _brandRepository.GetProductsByBrandIdAsync(brandId);
    }
}