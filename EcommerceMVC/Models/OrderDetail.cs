using System.Net.Mime;

namespace EcommerceMVC.Data.Models;

public class OrderDetail
{
    public string OrderDetailId { get; set; }
    public string OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public decimal TotalPrice => Quantity * UnitPrice;

    public OrderDetail() { }

    public OrderDetail(CartItem item, string orderId)
    {
        OrderDetailId = Guid.NewGuid().ToString();
        OrderId = orderId;
        ProductId = item.ProductId;
        Quantity = item.Quantity;
        UnitPrice = item.UnitPrice;
    }

    public Product Product { get; set; }
}