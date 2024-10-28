using EcommerceMVC.Data;
using EcommerceMVC.Helpers;
using Microsoft.EntityFrameworkCore;
using EcommerceMVC.Services;

namespace EcommerceMVC.Services.Implementations;

public class PaginationService<T> where T : class
{
    private readonly EcommerceDbContext _context;

    public PaginationService(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<T>> GetPaginatedData(IQueryable<T> source, int page, int pageSize)
    {
        var totalItems = await source.CountAsync();
        var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedResult<T>(items, totalItems, page, pageSize);
    }
}