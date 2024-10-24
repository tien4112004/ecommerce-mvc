using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Data.Views.ViewModels
{
	public class CartViewModel
	{
		public List<CartItem> CartItems { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal GrandTotal { get; set; }
	}
}
