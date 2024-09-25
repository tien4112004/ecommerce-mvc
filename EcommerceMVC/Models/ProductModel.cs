using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models
{
	public class ProductModel
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Required")]
		public string Name { get; set; }
		[Required]
		public string Slug { get; set; }
		[Required(ErrorMessage = "Required")]
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string[] Images { get; set; } = new string[] { };
	
		public int BrandId { get; set; }
		public int CategoryId { get; set; }

		public CategoryModel Category { get; set; }
		public BrandModel Brand { get; set; }
	}
}