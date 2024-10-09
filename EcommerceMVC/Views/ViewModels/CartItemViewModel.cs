using EcommerceMVC.Data.Models;

namespace EcommerceMVC.Data.Views.ViewModels
{
	public class CartItemViewModel
	{
		public List<CartItem> CartItems { get; set; }
		public decimal GrandTotal { get; set; }
	}
}
