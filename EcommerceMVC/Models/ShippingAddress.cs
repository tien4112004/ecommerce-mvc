using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Data.Models;

public class ShippingAddress
{
    public string FullName { get; set; }
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string District{ get; set; }
    public string Ward { get; set; }
    public string DetailAddress { get; set; }
    public bool IsDefault { get; set; }
    public string FullAddress => $"{DetailAddress}, {Ward}, {District}, {City}, {Country}";
}