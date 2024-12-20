﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EcommerceMVC.Data.Validations;

namespace EcommerceMVC.Data.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Product name is required")]
		public string Name { get; set; }
		public string Slug { get; set; }
		[Required(ErrorMessage = "Product description is required")]
		public string Description { get; set; }
		[Required]
		public decimal Price { get; set; }
		public string Image { get; set; }
		public int Status { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
		
		[NotMapped]
		[FileExtension]
		public IFormFile ImageUpload { get; set; }

		[Required, Range(1, int.MaxValue, ErrorMessage = "Brand has to be selected")]
		public int BrandId { get; set; }
		[Required, Range(1, int.MaxValue, ErrorMessage = "Category has to be selected")]
		public int CategoryId { get; set; }


		public Category Category { get; set; }
		public Brand Brand { get; set; }
	}

	public static class ProductStatus
	{
		public const int Active = 1;
		public const int Inactive = 0;
		public const int Discontinued = 2;
	}
}