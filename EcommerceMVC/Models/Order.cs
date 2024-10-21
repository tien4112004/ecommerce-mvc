using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace EcommerceMVC.Data.Models;

public class Order
{
    public string OrderId { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedTime { get; set; }
    public decimal TotalAmount { get; set; }
    public int Status { get; set; }

    // Delivery info
    public string FullName { get; set; }
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Ward { get; set; }
    public string DetailAddress { get; set; }
    public string Note { get; set; }
    public string FullAddress => $"{DetailAddress}, {Ward}, {District}, {City}, {Country}";

    public List<OrderDetail> OrderDetails { get; set; }

    public Order(string userId)
    {
        OrderId = Guid.NewGuid().ToString();
        CreatedTime = DateTime.UtcNow;
        UserId = userId;
        Status = OrderStatus.NOTPAID;
        OrderDetails = new List<OrderDetail>();
        TotalAmount = 0;
    }

    public Order(string userId, ShippingAddress address, string note)
    {
        OrderId = Guid.NewGuid().ToString();
        CreatedTime = DateTime.UtcNow;
        UserId = userId;
        Status = OrderStatus.NOTPAID;
        OrderDetails = new List<OrderDetail>();
        TotalAmount = 0;
        FullName = address.FullName;
        PhoneNumber = address.PhoneNumber;
        Country = address.Country;
        City = address.City;
        District = address.District;
        Ward = address.Ward;
        DetailAddress = address.DetailAddress;
        Note = note;
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