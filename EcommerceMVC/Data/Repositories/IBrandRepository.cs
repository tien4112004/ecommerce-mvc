﻿using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Data.Data.Repositories
{
    public interface IBrandRepository
    {
        Task<BrandModel?> GetBrandBySlugAsync(string slug);        
    }
}