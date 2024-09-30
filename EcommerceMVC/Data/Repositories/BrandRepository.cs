﻿using EcommerceMVC.Data.Models;
using EcommerceMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data.Data.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly EcommerceDBContext _context;
    
    public BrandRepository(EcommerceDBContext context)
    {
        _context = context;
    }
    
    public async Task<BrandModel?> GetBrandBySlugAsync(string slug)
    {
        return await _context.Brands.FirstOrDefaultAsync(brand => brand.Slug == slug);
    }
}