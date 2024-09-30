
using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Data.Models
{
	public class BrandModel
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
		public string? Image { get; set; }
	}
}
