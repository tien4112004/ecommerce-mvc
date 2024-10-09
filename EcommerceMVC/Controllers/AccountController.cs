using EcommerceMVC.Data.Models;
using EcommerceMVC.Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<IActionResult> Index()
		{
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login");
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(UserRegisterDto model)
		{
			if (ModelState.IsValid)
			{
				var user = new User { UserName = model.UserName, Email = model.Email };
				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(user, UserRoles.User);
					await _signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToAction("Index", "Home");
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(UserLoginDto model)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}

				ModelState.AddModelError(string.Empty, "Invalid login attempt.");
			}

			return View(model);
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
