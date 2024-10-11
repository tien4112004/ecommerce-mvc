using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data.Components;

public class CategoriesViewComponent : ViewComponent
{
    private readonly EcommerceDbContext _context;

    public CategoriesViewComponent(EcommerceDbContext context)
    {
        _context = context;
    } 
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return View(categories);
    }
}