using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Repository.Components;

public class BrandsViewComponent : ViewComponent
{
    private readonly EcommerceDBContext _context;

    public BrandsViewComponent(EcommerceDBContext context)
    {
        _context = context;
    } 
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _context.Brands.ToListAsync();
        return View(categories);
    }
}