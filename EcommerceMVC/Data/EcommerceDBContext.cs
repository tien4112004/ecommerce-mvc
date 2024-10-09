using EcommerceMVC.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data
{
	public class EcommerceDBContext : IdentityDbContext<UserModel>
	{
		public EcommerceDBContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<BrandModel> Brands { get; set; }
		public DbSet<ProductModel> Products { get; set; }
		public DbSet<CategoryModel> Categories { get; set; }
	}
}
