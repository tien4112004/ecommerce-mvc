using System.Net.Mime;

namespace EcommerceMVC.Data.Models;

public class OrderDetail
{
    public Guid OrderDetailId { get; set; }
    public Guid OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public decimal TotalPrice => Quantity * UnitPrice;

    public OrderDetail() { }

    public OrderDetail(CartItem item, Guid orderId)
    {
        OrderDetailId = new Guid();
        OrderId = orderId;
        ProductId = item.ProductId;
        Quantity = item.Quantity;
        UnitPrice = item.UnitPrice;
    }

    public Product Product { get; set; }
}