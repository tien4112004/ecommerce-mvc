using System.Security.Claims;
using EcommerceMVC.Data.Models;
using EcommerceMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers;

[Authorize]
[Route("Account/[Controller]/[Action]")]
public class SavedAddressController : Controller
{
    private readonly ISavedAddressService _addressService;

    public SavedAddressController(ISavedAddressService addressService)
    {
        _addressService = addressService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var addresses = await _addressService.GetByUserIdAsync(userId);
        return View(addresses);
    }

    [HttpGet]
    public IActionResult AddAddress()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAddress(ShippingAddress address)
    {
        if (ModelState.IsValid)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            address.UserId = userId;
            await _addressService.AddAsync(address);
            TempData["Success"] = "Address added successfully";
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Delete(Guid id)
    {
        var address = await _addressService.GetByIdAsync(id);
        if (address == null)
        {
            return NotFound();
        }

        await _addressService.DeleteAsync(id);
        TempData["Success"] = "Address deleted successfully";
        return RedirectToAction("Index");
    }
}