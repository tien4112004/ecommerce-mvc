namespace EcommerceMVC.Data.Models;

public class OrderDetail
{
    public Guid OrderDetailId { get; set; }
    public Guid OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public decimal TotalPrice => Quantity * UnitPrice;
}