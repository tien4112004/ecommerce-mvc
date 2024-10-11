using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data.Components;

public class BrandsViewComponent : ViewComponent
{
    private readonly EcommerceDbContext _context;

    public BrandsViewComponent(EcommerceDbContext context)
    {
        _context = context;
    } 
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _context.Brands.ToListAsync();
        return View(categories);
    }
}