namespace EcommerceMVC.Models
{
	public class CartItemModel
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal TotalPrice => UnitPrice * Quantity;
		public string Image { get; set; }


		public CartItemModel()
		{

		}

		public CartItemModel(ProductModel product, int quantity = 1)
		{
			ProductId = product.Id;
			ProductName = product.Name;
			UnitPrice = product.Price;
			Image = product.Image;
			Quantity = quantity;
		}
	}
}
