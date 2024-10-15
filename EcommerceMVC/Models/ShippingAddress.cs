using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Data.Models;

public class ShippingAddress
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
    public string Country { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string District{ get; set; }
    [Required]
    public string Ward { get; set; }
    [Required]
    public string DetailAddress { get; set; }
    public bool IsDefault { get; set; }
    public string FullAddress => $"{DetailAddress}, {Ward}, {District}, {City}, {Country}";
    public Guid UserId { get; set; }

    public User User { get; set; }
}
