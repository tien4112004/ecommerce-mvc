using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace EcommerceMVC.Data.Models;

public class Order
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedTime { get; set; }
    public decimal TotalAmount { get; set; }
    public int Status { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }

    public Order(Guid userId)
    {
        OrderId = Guid.NewGuid();
        CreatedTime = DateTime.UtcNow;
        UserId = userId;
        Status = OrderStatus.NOTPAID;
        OrderDetails = new List<OrderDetail>();
        TotalAmount = 0;
    }
}

public static class OrderStatus
{
    public const int NOTPAID = 0;
    public const int ORDERED = 1;
    public const int CONFIRMED = 2;
    public const int PREPARED = 3;
    public const int SHIPPED = 4;
    public const int DELIVERED = 5;
    public const int COMPLETED = 6;
    public const int CANCELLED = 7;
}