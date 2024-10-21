using Azure.Identity;
using EcommerceMVC.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EcommerceMVC.Data;

public class DatabaseSeeder
{
    public static void SeedingData(EcommerceDbContext context)
    {
        context.Database.Migrate();
        
        if (!context.Users.Any())
        {
            var passwordHasher = new PasswordHasher<User>();

            User admin = new User
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
            };
            admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin@123");

            User user = new User
            {
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@example.com",
                NormalizedEmail = "USER@EXAMPLE.COM",
                EmailConfirmed = true,
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "User@123");

            context.Users.AddRange(admin, user);
        }

        if (!context.Products.Any())
        {
            Category laptop = new Category
            {
                Name = "Laptop",
                Slug = "laptop",
                Description = "Laptop Category",
                Status = 1,
            };

            Category phone = new Category
            {
                Name = "Phone",
                Slug = "phone",
                Description = "Phone Category",
                Status = 1,
            };

            Brand apple = new Brand
            {
                Name = "Apple",
                Slug = "apple",
                Description = "Apple Products, including iPhone, iPad, MacBook, iMac, and more.",
                Status = 1,
            };

            Brand samsung = new Brand
            {
                Name = "Samsung",
                Slug = "samsung",
                Description = "Samsung Products, including Galaxy, Note, Tab, and more.",
                Status = 1,
            };

            context.Products.AddRange(
                new Product
                {
                    Name = "Macbook Air",
                    Slug = "macbook-air",
                    Description = "This is a sample product.",
                    Category = laptop,
                    Brand = apple,
                    Price = 1199
                },
                new Product
                {
                    Name = "iPhone 13",
                    Slug = "iPhone-13",
                    Description = "This is a sample product.",
                    Category = phone,
                    Brand = apple,
                    Price = 799
                },
                new Product
                {
                    Name = "Galaxy S21",
                    Slug = "galaxy-s21",
                    Description = "This is a sample product.",
                    Category = phone,
                    Brand = samsung,
                    Price = 999
                });
        }

        context.SaveChanges();
    }
}