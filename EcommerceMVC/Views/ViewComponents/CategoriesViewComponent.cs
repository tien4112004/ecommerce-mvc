using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Repository.Components;

public class CategoriesViewComponent : ViewComponent
{
    private readonly EcommerceDBContext _context;

    public CategoriesViewComponent(EcommerceDBContext context)
    {
        _context = context;
    } 
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return View(categories);
    }
}