using System.ComponentModel;
using System.Net.Mime;
using EcommerceMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Repository;

public class SeedData
{
    public static void SeedingData(EcommerceDBContext context)
    {
        context.Database.Migrate();
        if (!context.Products.Any())
        {
            CategoryModel laptop = new CategoryModel
            {
                Name = "Laptop",
                Slug = "laptop",
                Description = "Laptop Category",
                Status = 1,
            };

            CategoryModel phone = new CategoryModel
            {
                Name = "Phone",
                Slug = "phone",
                Description = "Phone Category",
                Status = 1,
            };

            BrandModel apple = new BrandModel
            {
                Name = "Apple",
                Slug = "apple",
                Description = "Apple Products, including iPhone, iPad, MacBook, iMac, and more.",
                Status = 1,
            };

            BrandModel samsung = new BrandModel
            {
                Name = "Samsung",
                Slug = "samsung",
                Description = "Samsung Products, including Galaxy, Note, Tab, and more.",
                Status = 1,
            };

            context.Products.AddRange(
                new ProductModel
                {
                    Name = "Macbook Air", Slug = "macbook-air", Description = "This is a sample product.", Category = laptop,
                    Brand = apple, Price = 1199
                },
                new ProductModel
                {
                    Name = "iPhone 13", Slug = "iPhone-13", Description = "This is a sample product.", Category = phone,
                    Brand = apple, Price = 799
                },
                new ProductModel
                {
                    Name = "Galaxy S21", Slug = "galaxy-s21", Description = "This is a sample product.", Category = phone,
                    Brand = samsung, Price = 999
                });
        }

        context.SaveChanges();
    }
}