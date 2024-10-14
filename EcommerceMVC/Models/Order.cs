using System.ComponentModel.DataAnnotations;
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
    
    // Delivery info
    public string FullName { get; set; }
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string District{ get; set; }
    public string Ward { get; set; }
    public string DetailAddress { get; set; }
    public string Note { get; set; }
    public string FullAddress => $"{DetailAddress}, {Ward}, {District}, {City}, {Country}";

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
    
    public Order(Guid userId, string fullName, string phoneNumber, string country, string city, string district, string ward, string detailAddress, string note)
    {
        OrderId = Guid.NewGuid();
        CreatedTime = DateTime.UtcNow;
        UserId = userId;
        Status = OrderStatus.NOTPAID;
        OrderDetails = new List<OrderDetail>();
        TotalAmount = 0;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Country = country;
        City = city;
        District = district;
        Ward = ward;
        DetailAddress = detailAddress;
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