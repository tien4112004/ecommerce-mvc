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

			modelBuilder.Entity<Product>()
				.Property(p => p.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			modelBuilder.Entity<Product>()
				.Property(p => p.UpdatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			modelBuilder.Entity<Product>()
				.Property(p => p.UpdatedAt)
				.ValueGeneratedOnAddOrUpdate()
				.HasDefaultValueSql("GETUTCDATE()");
			
			modelBuilder.Entity<Order>()
				.HasMany(o => o.OrderDetails)
				.WithOne()
				.HasForeignKey(od => od.OrderId)
				.OnDelete(DeleteBehavior.Cascade);
		}
		
		public override int SaveChanges()
		{
			foreach (var entry in ChangeTracker.Entries<Product>())
			{
				if (entry.State == EntityState.Modified)
				{
					entry.Entity.UpdatedAt = DateTime.UtcNow;
				}
			}
			return base.SaveChanges();
		}

		public DbSet<Brand> Brands { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<ShippingAddress> SavedAddresses { get; set; }
	}
}
