using System.IO;
using X.PagedList;
using OfficeOpenXml;
using EntityLayer.Dtos;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebUILayer.Areas.Admin.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessLayer.ValidationRules.FluentValidation;
using DocumentFormat.OpenXml.Spreadsheet;

namespace WebUILayer.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UserController : Controller
	{
		private readonly IRoleService _roleService;
		private readonly IUserService _userService;
		private readonly INotyfService _notyfService;

		public UserController(IRoleService roleService, IUserService userService, INotyfService notyfService)
		{
			_roleService = roleService;
			_userService = userService;
			_notyfService = notyfService;
		}

		public IActionResult Index(int page = 1)
		{
			var values = _userService.GetList().Result.ToPagedList(page, 5);
			return View(values);
		}

		public IActionResult Detail(int id)
		{
			var values = _userService.GetById(id).Result;
			return View(values);
		}

		[HttpGet]
		public async Task<IActionResult> AssignRole(int id)
		{
			var user = await _userService.GetById(id);
			ViewBag.userName = user.NameSurname;
			TempData["userId"] = user.Id;
			var roles = await _roleService.GetList();
			var userRoles = await _userService.GetRoles(user);
			List<RoleAssignViewModel> roleAssignViewModels = new();
			foreach (var role in roles)
			{
				RoleAssignViewModel roleAssignViewModel = new()
				{
					RoleId = role.Id,
					RoleName = role.Name,
					RoleExist = userRoles.Contains(role.Name)
				};
				roleAssignViewModels.Add(roleAssignViewModel);
			}
			return View(roleAssignViewModels);
		}

		[HttpPost]
		public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> roleAssignViewModels)
		{
			var userId = (int)TempData["userId"];
			var user = _userService.GetById(userId);
			foreach (var role in roleAssignViewModels)
			{
				if (role.RoleExist)
				{
					await _userService.AddRole(user, role.RoleName);
				}
				else
				{
					await _userService.DeleteRole(user, role.RoleName);
				}
			}
			return RedirectToAction("Index", "User");
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(UserListDto userListDto)
		{
			UserValidator userValidator = new();
			ValidationResult validationResult = userValidator.Validate(userListDto);
			if (validationResult.IsValid)
			{
				await _userService.Add(userListDto);
				var getUser = _userService.GetByEmail(userListDto.Email);
				await _userService.AddRole(getUser, "Member");
				_notyfService.Success("Kullanıcı Eklendi");
				return RedirectToAction("Index", "User");
			}
			else
			{
				foreach (var item in validationResult.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
				_notyfService.Error("Kullanıcı Eklenemedi");
				return View();
			}
		}

		public async Task<IActionResult> ChangeStatus(int id)
		{
			await _userService.ChangeStatus(id);
			_notyfService.Success("Durum Değiştirildi");
			return RedirectToAction("Index", "User");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var user = await _userService.GetById(id);
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
				await _userService.UpdateMember(userListDto);
				_notyfService.Success("Kullanıcı Güncellendi");
				return RedirectToAction("Index", "User");
			}
			else
			{
				foreach (var item in validationResult.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
				_notyfService.Error("Kullanıcı Güncellenemedi");
				return View();
			}
		}

		public async Task<IActionResult> Delete(int id)
		{
			if (id != 0)
			{
				var values = await _userService.GetById(id);
				bool isInRole = await _userService.IsInRole(values, "Member");
				if (isInRole)
				{
					var getUser = _userService.GetByEmail(values.Email);
					await _userService.DeleteRole(getUser, "Member");
				}
				await _userService.Delete(values);
				_notyfService.Success("Kullanıcı Silindi");
				return RedirectToAction("Index", "User");
			}
			else
			{
				_notyfService.Error("Kullanıcı Silinemedi");
				return View();
			}
		}

		public async Task<IActionResult> UserExcelList()
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			ExcelPackage excelPackage = new();
			var workSheet = excelPackage.Workbook.Worksheets.Add("Kullanıcı Listesi");
			workSheet.Cells["A1"].LoadFromCollection(await _userService.GetList(), true, OfficeOpenXml.Table.TableStyles.Light10);
			workSheet.Cells[1, 1].Value = "No";
			workSheet.Cells[1, 2].Value = "Görsel";
			workSheet.Cells[1, 3].Value = "Adı Soyadı";
			workSheet.Cells[1, 4].Value = "Kullanıcı Adı";
			workSheet.Cells[1, 5].Value = "Hakkında";
			workSheet.Cells[1, 6].Value = "Durum";
			int rowCount = 2, count = 1;
			foreach (var user in await _userService.GetList())
			{
				workSheet.Cells[rowCount, 1].Value = count++;
				workSheet.Cells[rowCount, 2].Value = "/images/user/" + user.UserImage;
				workSheet.Cells[rowCount, 3].Value = user.NameSurname;
				workSheet.Cells[rowCount, 4].Value = user.UserName;
				workSheet.Cells[rowCount, 5].Value = user.AboutUser;
				workSheet.Cells[rowCount, 6].Value = (user.Status == true ? "Aktif" : "Pasif");
				rowCount++;
			}
			var stream = new MemoryStream();
			excelPackage.SaveAs(stream);
			var content = stream.ToArray();
			return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheets", "Traversal | Kullanıcı Listesi.xlsx");
		}
	}
}