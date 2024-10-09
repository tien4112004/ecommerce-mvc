using EcommerceMVC.Data;
using EcommerceMVC.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Areas.Admin.Services;

public class BrandService : IBrandService
{
    private readonly EcommerceDBContext _context;
    
    public BrandService(EcommerceDBContext context)
    {
        _context = context;
    }
    
    public async Task<Brand?> GetBrandBySlugAsync(string slug)
    {
        return await _context.Brands.FirstOrDefaultAsync(c => c.Slug == slug);
    }
    
    public async Task<Brand?> GetBrandByIdAsync(int brandId)
    {
        return await _context.Brands.FirstOrDefaultAsync(c => c.Id == brandId);
    }
        
    public async Task<List<Brand>> GetAllBrandsAsync()
    {
        return await _context.Brands.ToListAsync();
    }
        
    public async Task CreateBrandAsync(Brand brand)
    {
        brand.Slug = brand.Name.Replace(" ", "-");
        var slug = await _context.Brands.FirstOrDefaultAsync(c => c.Slug == brand.Slug);
        if (slug != null)
        {
            throw new InvalidOperationException("A brand with this slug already exists.");
        }

        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateBrandAsync(int brandId, Brand brand)
    {
        var existingBrand = await _context.Brands.FindAsync(brandId);
        if (existingBrand == null)
        {
            throw new InvalidOperationException("Brand not found.");
        }
    
        existingBrand.Name = brand.Name;
        existingBrand.Description = brand.Description;
        existingBrand.Slug = brand.Name.Replace(" ", "-");
        existingBrand.Status = brand.Status;
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteBrandAsync(int brandId)
    {
        var brand = await _context.Brands.FindAsync(brandId);
        if (brand == null)
        {
            throw new InvalidOperationException("Brand not found.");
        }
    
        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync();
    }
}