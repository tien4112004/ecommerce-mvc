using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceMVC.Data.Data.Repositories;
using EcommerceMVC.Data.Models;
using EcommerceMVC.Data;

namespace EcommerceMVC.Data.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository, EcommerceDBContext context)
        {
            _brandRepository = brandRepository;
        }

        public async Task<BrandModel?> GetBrandBySlugAsync(string slug)
        {
            return await _brandRepository.GetBrandBySlugAsync(slug);
        }

        public async Task<List<ProductModel>> GetProductsByBrandIdAsync(int brandId)
        {
            return await _brandRepository.GetProductsByBrandIdAsync(brandId);
        }
    }
}