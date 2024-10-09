using EcommerceMVC.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data
{
	public class EcommerceDBContext : IdentityDbContext<User>
	{
		public EcommerceDBContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Brand> Brands { get; set; }
		public DbSet<ProductModel> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
	}
}
