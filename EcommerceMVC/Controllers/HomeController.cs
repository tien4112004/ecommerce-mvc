using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EcommerceMVC.Data.Models;
using EcommerceMVC.Data;

namespace EcommerceMVC.Data.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EcommerceDbContext _context;
        
        public HomeController(ILogger<HomeController> logger, EcommerceDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
