using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EcommerceMVC.Data.Models;

public class User : IdentityUser
{
    public string? FirstName { get; set; }    
    public string? LastName { get; set; }
}

public static class UserRoles
{
    public const string Administrator = "Administrator";
    public const string Manager = "Manager";
    public const string User = "User";
}