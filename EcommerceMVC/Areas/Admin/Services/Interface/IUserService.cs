using EcommerceMVC.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceMVC.Areas.Admin.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(string userId);
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(string userId, User user);
    Task SetUserPasswordAsync(string userId, string newPassword);
    Task LockUserAsync(string userId);
    Task UnlockUserAsync(string userId);
}