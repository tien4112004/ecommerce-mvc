using Microsoft.AspNetCore.Mvc;
using EcommerceMVC.Helpers;

namespace EcommerceMVC.ViewComponents
{
    public class PaginationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PagedResult<object> model)
        {
            return View(model);
        }
    }
}