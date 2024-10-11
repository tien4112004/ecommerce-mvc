using EcommerceMVC.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data
{
	public class EcommerceDbContext : IdentityDbContext<User>
	{
		public EcommerceDbContext() : base(new DbContextOptions<EcommerceDbContext>())
		{
		}
		
		public EcommerceDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Order>()
				.HasMany(o => o.OrderDetails)
				.WithOne()
				.HasForeignKey(od => od.OrderId)
				.OnDelete(DeleteBehavior.Cascade);
		}

		public DbSet<Brand> Brands { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
	}
}
