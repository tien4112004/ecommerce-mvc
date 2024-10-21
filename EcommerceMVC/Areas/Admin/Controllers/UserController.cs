using System;
using EcommerceMVC.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EcommerceMVC.Areas.Admin.Services;

namespace EcommerceMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = UserRoles.Administrator)]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _userService.GetAllUsersAsync();
        return View(orders);
    }

    [HttpPost]
    public async Task<IActionResult> LockUser([FromForm] string userId)
    {
        try
        {
            await _userService.LockUserAsync(userId);
            TempData["Success"] = "User has been locked successfully.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error locking user: {ex.Message}";
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> UnlockUser([FromForm] string userId)
    {
        try
        {
            await _userService.UnlockUserAsync(userId);
            TempData["Success"] = "User has been unlocked successfully.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error unlocking user: {ex.Message}";
        }
        return RedirectToAction("Index");
    }
}