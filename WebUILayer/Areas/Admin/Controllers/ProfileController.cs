using EntityLayer.Dtos;
using BusinessLayer.Abstract;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessLayer.ValidationRules.FluentValidation;

namespace WebUILayer.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ProfileController : Controller
	{
		private readonly INotyfService _notyfService;
		private readonly IUserService _userService;

		public ProfileController(INotyfService notyfService, IUserService userService)
		{
			_notyfService = notyfService;
			_userService = userService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var values = _userService.GetByUser();
			return View(values);
		}

		[HttpGet]
		public IActionResult Edit()
		{
			var values = _userService.GetByUser();
			UserListDto userListDto = new()
			{
				Id = values.Id,
				Image = values.UserImage,
				NameSurname = values.NameSurname,
				AboutUser = values.AboutUser,
				Email = values.Email
			};
			return View(userListDto);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(UserListDto userListDto)
		{
			UserValidator userValidator = new();
			ValidationResult validationResult = userValidator.Validate(userListDto);
			if (validationResult.IsValid)
			{
				userListDto.Status = true;
				await _userService.Update(userListDto);
				_notyfService.Success("Profil güncellendi");
				return RedirectToAction("Index", "Profile");
			}
			else
			{
				foreach (var item in validationResult.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
				return View();
			}
		}
	}
}