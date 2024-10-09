using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace EcommerceMVC.Dtos.UserDtos;

public class UserRegisterDto
{
    [Required, MinLength(5)]
    public string UserName { get; set; }
    [Required, DataType(DataType.Password)]
    public string Password { get; set; }
    [Required, DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}