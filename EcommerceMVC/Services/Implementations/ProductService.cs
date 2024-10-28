using EcommerceMVC.Data;
using EcommerceMVC.Data.Models;
using Microsoft.EntityFrameworkCore;
using EcommerceMVC.Helpers;

namespace EcommerceMVC.Services.Implementations;

public class ProductService : IProductService
{
    private readonly EcommerceDbContext _context;
    private readonly PaginationService<Product> _paginationService;

    public ProductService(EcommerceDbContext context, PaginationService<Product> paginationService)
    {
        _context = context;
        _paginationService = paginationService;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products
                        .Where(p => p.Status != ProductStatus.Inactive)
                        .ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
    {
        return await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
    }

    public async Task<PagedResult<Product>> GetPaginatedProductsAsync(int page, int pageSize)
    {
        var query = _context.Products.AsQueryable();
        return await _paginationService.GetPaginatedData(query, page, pageSize);
    }
}