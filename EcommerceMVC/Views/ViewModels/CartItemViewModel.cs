using EcommerceMVC.Models;

namespace EcommerceMVC.Views.ViewModels
{
	public class CartItemViewModel
	{
		public List<CartItemModel> CartItems { get; set; }
		public decimal GrandTotal { get; set; }
	}
}
