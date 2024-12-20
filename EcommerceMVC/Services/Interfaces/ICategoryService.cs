﻿using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Services;

public interface ICategoryService
{
    /// <summary>
    /// Gets a category by its slug.
    /// </summary>
    /// <param name="slug">The category slug.</param>
    /// <returns>The category model, or null if not found.</returns>
    Task<Category?> GetCategoryBySlugAsync(string slug);

    /// <summary>
    /// Gets products by category ID.
    /// </summary>
    /// <param name="categoryId">The category ID.</param>
    /// <returns>A list of product models.</returns>
    Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
}