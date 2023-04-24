using System;
using System.IO;
using X.PagedList;
using OfficeOpenXml;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessLayer.ValidationRules.FluentValidation;

namespace WebUILayer.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ContactController : Controller
	{
		private readonly IContactService _contactService;
		private readonly INotyfService _notyfService;

		public ContactController(IContactService contactService, INotyfService notyfService)
		{
			_contactService = contactService;
			_notyfService = notyfService;
		}

		public IActionResult Index(int page = 1)
		{
			var result = _contactService.GetList().ToPagedList(page, 5);
			return View(result);
		}

		public IActionResult ChangeStatus(int id)
		{
			_contactService.ChangeStatus(id);
			_notyfService.Success("Durum güncellendi");
			return RedirectToAction("Index", "Contact");
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(Contact contact)
		{
			ContactValidator contactValidator = new();
			ValidationResult validationResult = contactValidator.Validate(contact);
			if (validationResult.IsValid)
			{
				contact.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
				contact.Status = true;
				_contactService.Add(contact);
				_notyfService.Success("İletişim eklendi");
				return RedirectToAction("Index", "Contact");
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

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var values = _contactService.GetById(id);
			return View(values);
		}

		[HttpPost]
		public IActionResult Edit(Contact contact)
		{
			ContactValidator contactValidator = new();
			ValidationResult validationResult = contactValidator.Validate(contact);
			if (validationResult.IsValid)
			{
				_contactService.Update(contact);
				_notyfService.Success("İletişim güncellendi");
				return RedirectToAction("Index", "Contact");
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

		public IActionResult Delete(int id)
		{
			var values = _contactService.GetById(id);
			_contactService.Delete(values);
			_notyfService.Success("İletişim silindi");
			return RedirectToAction("Index", "Contact");
		}

		public IActionResult ExcelDownload()
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			ExcelPackage excelPackage = new();
			var workSheet = excelPackage.Workbook.Worksheets.Add("İletişim Listesi");
			workSheet.Cells["A1"].LoadFromCollection(_contactService.GetList(), true, OfficeOpenXml.Table.TableStyles.Light10);
			workSheet.Cells[1, 1].Value = "No";
			workSheet.Cells[1, 2].Value = "Adı Soyadı";
			workSheet.Cells[1, 3].Value = "Email";
			workSheet.Cells[1, 4].Value = "Konu";
			workSheet.Cells[1, 5].Value = "Mesaj";
			workSheet.Cells[1, 6].Value = "Tarih";
			workSheet.Cells[1, 7].Value = "Durum";
			int rowCount = 2, count = 1;
			foreach (var item in _contactService.GetList())
			{
				workSheet.Cells[rowCount, 1].Value = count++;
				workSheet.Cells[rowCount, 2].Value = item.NameSurname;
				workSheet.Cells[rowCount, 3].Value = item.Email;
				workSheet.Cells[rowCount, 4].Value = item.Subject;
				workSheet.Cells[rowCount, 5].Value = item.Message;
				workSheet.Cells[rowCount, 6].Value = item.Date.ToShortDateString();
				workSheet.Cells[rowCount, 7].Value = (item.Status == true ? "Aktif" : "Pasif");
				rowCount++;
			}
			var stream = new MemoryStream();
			excelPackage.SaveAs(stream);
			var content = stream.ToArray();
			return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheets", "Traversal | İletişim Listesi.xlsx");
		}
	}
}