using System.ComponentModel;
using System.Net.Mime;
using EcommerceMVC.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Data;

public class DatabaseSeeder
{
    public static void SeedingData(EcommerceDBContext context)
    {
        context.Database.Migrate();
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
                    Name = "Macbook Air", Slug = "macbook-air", Description = "This is a sample product.", Category = laptop,
                    Brand = apple, Price = 1199
                },
                new Product
                {
                    Name = "iPhone 13", Slug = "iPhone-13", Description = "This is a sample product.", Category = phone,
                    Brand = apple, Price = 799
                },
                new Product
                {
                    Name = "Galaxy S21", Slug = "galaxy-s21", Description = "This is a sample product.", Category = phone,
                    Brand = samsung, Price = 999
                });
        }

        context.SaveChanges();
    }
}