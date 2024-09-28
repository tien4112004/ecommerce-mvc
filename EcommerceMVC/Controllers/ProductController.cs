using EcommerceMVC.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Controllers
{
	public class ProductController : Controller
	{
		private readonly EcommerceDBContext _context;

		public ProductController(EcommerceDBContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> Detail(int? productId)
		{
			if (productId == null) return Redirect("Index");
			var product = await _context.Products.Where(product => product.Id == productId).FirstOrDefaultAsync();
			if (product == null)
			{
				return Redirect("/404");
			}
			return View(product);
		}
	}
}
