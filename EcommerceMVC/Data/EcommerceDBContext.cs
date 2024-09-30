using EcommerceMVC.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data
{
	public class EcommerceDBContext : DbContext
	{
		public EcommerceDBContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<BrandModel> Brands { get; set; }
		public DbSet<ProductModel> Products { get; set; }
		public DbSet<CategoryModel> Categories { get; set; }
	}
}
