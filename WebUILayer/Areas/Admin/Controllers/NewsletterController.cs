using X.PagedList;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessLayer.ValidationRules.FluentValidation;
using FluentValidation.Results;
using System;
using OfficeOpenXml;
using System.IO;

namespace WebUILayer.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class NewsletterController : Controller
	{
		private readonly INewsletterService _newsletterService;
		private readonly INotyfService _notyfService;

		public NewsletterController(INewsletterService newsletterService, INotyfService notyfService)
		{
			_newsletterService = newsletterService;
			_notyfService = notyfService;
		}

		public IActionResult Index(int page = 1)
		{
			var result = _newsletterService.GetList().ToPagedList(page, 5);
			return View(result);
		}

		public IActionResult ChangeStatus(int id)
		{
			_newsletterService.ChangeStatus(id);
			_notyfService.Success("Durum güncellendi");
			return RedirectToAction("Index", "Newsletter");
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(Newsletter newsletter)
		{
			NewsletterValidator newsletterValidator = new();
			ValidationResult validationResult = newsletterValidator.Validate(newsletter);
			if (validationResult.IsValid)
			{
				newsletter.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
				newsletter.Status = true;
				_newsletterService.Add(newsletter);
				_notyfService.Success("Abonelik eklendi");
				return RedirectToAction("Index", "Newsletter");
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
			var values = _newsletterService.GetById(id);
			return View(values);
		}

		[HttpPost]
		public IActionResult Edit(Newsletter newsletter)
		{
			NewsletterValidator newsletterValidator = new();
			ValidationResult validationResult = newsletterValidator.Validate(newsletter);
			if (validationResult.IsValid)
			{
				_newsletterService.Update(newsletter);
				_notyfService.Success("Abonelik güncellendi");
				return RedirectToAction("Index", "Newsletter");
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
			var values = _newsletterService.GetById(id);
			_newsletterService.Delete(values);
			_notyfService.Success("Abonelik silindi");
			return RedirectToAction("Index", "Newsletter");
		}

		public IActionResult ExcelDownload()
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			ExcelPackage excelPackage = new();
			var workSheet = excelPackage.Workbook.Worksheets.Add("Abonelik Listesi");
			workSheet.Cells["A1"].LoadFromCollection(_newsletterService.GetList(), true, OfficeOpenXml.Table.TableStyles.Light10);
			workSheet.Cells[1, 1].Value = "No";
			workSheet.Cells[1, 2].Value = "Email";
			workSheet.Cells[1, 3].Value = "Tarih";
			workSheet.Cells[1, 4].Value = "Durum";
			int rowCount = 2, count = 1;
			foreach (var item in _newsletterService.GetList())
			{
				workSheet.Cells[rowCount, 1].Value = count++;
				workSheet.Cells[rowCount, 2].Value = item.Email;
				workSheet.Cells[rowCount, 3].Value = item.Date.ToShortDateString();
				workSheet.Cells[rowCount, 4].Value = (item.Status == true ? "Aktif" : "Pasif");
				rowCount++;
			}
			var stream = new MemoryStream();
			excelPackage.SaveAs(stream);
			var content = stream.ToArray();
			return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheets", "Traversal | Abonelik Listesi.xlsx");
		}
	}
}