using System;
using System.IO;
using System.Linq;
using X.PagedList;
using OfficeOpenXml;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessLayer.ValidationRules.FluentValidation;

namespace WebUILayer.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CommentController : Controller
	{
		private readonly ICommentService _commentService;
		private readonly IDestinationService _destinationService;
		private readonly INotyfService _notyfService;
		private readonly IUserService _userService;

		public CommentController(ICommentService commentService, IDestinationService destinationService, INotyfService notyfService, IUserService userService)
		{
			_commentService = commentService;
			_destinationService = destinationService;
			_notyfService = notyfService;
			_userService = userService;
		}

		public IActionResult Index(int page = 1)
		{
			var result = _commentService.GetCommentListWithDestinationAndUser().ToPagedList(page, 5);
			return View(result);
		}

		public IActionResult ChangeStatus(int id)
		{
			_commentService.ChangeStatus(id);
			_notyfService.Success("Durum güncellendi");
			return RedirectToAction("Index", "Comment");
		}

		[HttpGet]
		public IActionResult Add()
		{
			List<SelectListItem> destinationValues = (from x in _destinationService.GetList()
													  select new SelectListItem
													  {
														  Text = x.City,
														  Value = x.Id.ToString()
													  }).ToList();
			ViewBag.destinationValues = destinationValues;
			return View();
		}

		[HttpPost]
		public IActionResult Add(Comment comment)
		{
			CommentValidator commentValidator = new();
			ValidationResult validationResult = commentValidator.Validate(comment);
			if (validationResult.IsValid)
			{
				comment.UserId = _userService.GetById();
				comment.UserImage = _userService.GetByUser().UserImage;
				comment.NameSurname = _userService.GetByUser().NameSurname;
				comment.CommentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
				comment.Status = true;
				_commentService.Add(comment);
				_notyfService.Success("Yorum eklendi");
				return RedirectToAction("Index", "Comment");
			}
			else
			{
				List<SelectListItem> destinationValues = (from x in _destinationService.GetList()
														  select new SelectListItem
														  {
															  Text = x.City,
															  Value = x.Id.ToString()
														  }).ToList();
				ViewBag.destinationValues = destinationValues;
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
			List<SelectListItem> destinationValues = (from x in _destinationService.GetList()
													  select new SelectListItem
													  {
														  Text = x.City,
														  Value = x.Id.ToString()
													  }).ToList();
			ViewBag.destinationValues = destinationValues;
			var values = _commentService.GetById(id);
			return View(values);
		}

		[HttpPost]
		public IActionResult Edit(Comment comment)
		{
			CommentValidator commentValidator = new();
			ValidationResult validationResult = commentValidator.Validate(comment);
			if (validationResult.IsValid)
			{
				comment.UserId = _userService.GetById();
				comment.UserImage = _userService.GetByUser().UserImage;
				_commentService.Update(comment);
				_notyfService.Success("Yorum güncellendi");
				return RedirectToAction("Index", "Comment");
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
			var result = _commentService.GetById(id);
			_commentService.Delete(result);
			_notyfService.Success("Yorum silindi");
			return RedirectToAction("Index", "Comment");
		}

		public IActionResult ExcelDownload()
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			ExcelPackage excelPackage = new();
			var workSheet = excelPackage.Workbook.Worksheets.Add("Yorum Listesi");
			workSheet.Cells["A1"].LoadFromCollection(_commentService.GetCommentListWithDestinationAndUser(), true, OfficeOpenXml.Table.TableStyles.Light10);
			workSheet.Cells[1, 1].Value = "No";
			workSheet.Cells[1, 2].Value = "Görsel";
			workSheet.Cells[1, 3].Value = "Adı Soyadı";
			workSheet.Cells[1, 4].Value = "Rota";
			workSheet.Cells[1, 5].Value = "Yorum İçeriği";
			workSheet.Cells[1, 6].Value = "Yorum Tarihi";
			workSheet.Cells[1, 7].Value = "Durum";
			int rowCount = 2, count = 1;
			foreach (var item in _commentService.GetCommentListWithDestinationAndUser())
			{
				workSheet.Cells[rowCount, 1].Value = count++;
				workSheet.Cells[rowCount, 2].Value = "/images/user/" + item.Destination.Image;
				workSheet.Cells[rowCount, 3].Value = item.NameSurname;
				workSheet.Cells[rowCount, 4].Value = item.Destination.City;
				workSheet.Cells[rowCount, 5].Value = item.CommentContent;
				workSheet.Cells[rowCount, 6].Value = item.CommentDate.ToShortDateString();
				workSheet.Cells[rowCount, 7].Value = (item.Status == true ? "Aktif" : "Pasif");
				rowCount++;
			}
			var stream = new MemoryStream();
			excelPackage.SaveAs(stream);
			var content = stream.ToArray();
			return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheets", "Traversal | Yorum Listesi.xlsx");
		}
	}
}