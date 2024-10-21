using EcommerceMVC.Data;
using EcommerceMVC.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMVC.Areas.Admin.Services;

public class UserService : IUserService
{
    private readonly EcommerceDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<UserService> _logger;


    public UserService(UserManager<User> userManager, ILogger<UserService> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(string userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(string userId, User user)
    {
        var existingUser = await _context.Users.FindAsync(userId);
        if (existingUser == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        existingUser.UserName = user.UserName;
        existingUser.Email = user.Email;
        existingUser.PhoneNumber = user.PhoneNumber;
        existingUser.EmailConfirmed = user.EmailConfirmed;
        existingUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
        existingUser.PasswordHash = user.PasswordHash;

        _context.Users.Update(existingUser);
        await _context.SaveChangesAsync();
    }


    public async Task LockUserAsync(string userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {userId} not found.");
                throw new InvalidOperationException("User not found.");
            }

            // var lockoutEndDate = DateTimeOffset.MaxValue;
            // var result = await _userManager.SetLockoutEndDateAsync(user, lockoutEndDate);
            var result = await _userManager.SetLockoutEnabledAsync(user, true);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to lock the user.");
            }

            _logger.LogInformation($"User with ID {userId} has been locked.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while locking the user with ID {userId}.");
            throw;
        }
    }

    public async Task UnlockUserAsync(string userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {userId} not found.");
                throw new InvalidOperationException("User not found.");
            }

            var lockResult = await _userManager.SetLockoutEnabledAsync(user, false);
            // var result = await _userManager.SetLockoutEndDateAsync(user, null);

            if (!lockResult.Succeeded)
            {
                throw new InvalidOperationException("Failed to unlock the user.");
            }

            _logger.LogInformation($"User with ID {userId} has been unlocked.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while unlocking the user with ID {userId}.");
            throw;
        }
    }


}