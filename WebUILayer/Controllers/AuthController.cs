﻿using EntityLayer.Dtos;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace WebUILayer.Controllers
{
	[AllowAnonymous]
	public class AuthController : Controller
	{
		private readonly INotyfService _notyfService;
		private readonly IUserService _userService;
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;

		public AuthController(INotyfService notyfService, IUserService userService, SignInManager<User> signInManager, UserManager<User> userManager)
		{
			_notyfService = notyfService;
			_userService = userService;
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(UserLoginDto userLoginDto)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(userLoginDto.Email, userLoginDto.Password, false, false);
				if (result.Succeeded)
				{
					var user = _userManager.FindByEmailAsync(userLoginDto.Email).Result;
					if (user.Status == false)
					{
						_notyfService.Error("Kullanıcı bulunamadı");
						return RedirectToAction("Login", "Auth");
					}
					var getRoles = _userManager.GetRolesAsync(user).Result;
					if (getRoles.Contains("Admin"))
					{
						_notyfService.Success("Hoşgeldiniz");
						return RedirectToAction("Index", "Home", new { area = "Admin" });
					}
					else
					{
						_notyfService.Success("Hoşgeldiniz");
						return RedirectToAction("Index", "Home", new { area = "Member" });
					}
				}
				else
				{
					_notyfService.Error("Kullanıcı adı veya şifre hatalı");
					return RedirectToAction("Login", "Auth");
				}
			}
			return View(userLoginDto);
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
		{
			if (ModelState.IsValid)
			{
				User user = new()
				{
					NameSurname = userRegisterDto.NameSurname,
					UserName = userRegisterDto.Email,
					Email = userRegisterDto.Email,
					Status = true
				};
				if (userRegisterDto.Password == userRegisterDto.ConfirmPassword)
				{
					var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
					if (result.Succeeded)
					{
						var getUser = _userService.GetByEmail(user.Email);
						await _userService.AddRole(getUser, "Member");
						_notyfService.Success("Kayıt başarılı");
						return RedirectToAction("Login", "Auth");
					}
					else
					{
						foreach (var item in result.Errors)
						{
							ModelState.AddModelError("", item.Description);
						}
					}
				}
			}
			return View(userRegisterDto);
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Login", "Auth");
		}
	}
}