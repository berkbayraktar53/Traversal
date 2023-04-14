using EntityLayer.Dtos;
using BusinessLayer.Abstract;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessLayer.ValidationRules.FluentValidation;

namespace WebUILayer.Areas.Member.Controllers
{
	[Area("Member")]
	[Authorize(Roles = "Member")]
	public class ProfileController : Controller
	{
		private readonly INotyfService _notyfService;
		private readonly IUserService _userService;

		public ProfileController(INotyfService notyfService, IUserService userService)
		{
			_notyfService = notyfService;
			_userService = userService;
		}

		public IActionResult Index()
		{
			var values = _userService.GetByUser();
			return View(values);
		}

		[HttpGet]
		public IActionResult Edit()
		{
			var user = _userService.GetByUser();
			UserListDto userListDto = new()
			{
				Id = user.Id,
				AboutUser = user.AboutUser,
				Email = user.Email,
				NameSurname = user.NameSurname,
				UserName = user.UserName,
				Status = user.Status
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
				_notyfService.Error("Profil güncellenemedi");
				return View();
			}
		}
	}
}