﻿using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models
{
	public class CategoryModel
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Required")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Required")]
		public string Description { get; set; }
		[Required]
		public string Slug { get; set; }
		public int Status { get; set; }
	}
}