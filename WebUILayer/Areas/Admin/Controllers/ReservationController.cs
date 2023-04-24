using System.IO;
using System.Linq;
using X.PagedList;
using OfficeOpenXml;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using System.Threading.Tasks;
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
	public class ReservationController : Controller
	{
		private readonly IDestinationService _destinationService;
		private readonly INotyfService _notyfService;
		private readonly IReservationService _reservationService;
		private readonly IUserService _userService;

		public ReservationController(IDestinationService destinationService, INotyfService notyfService, IReservationService reservationService, IUserService userService)
		{
			_destinationService = destinationService;
			_notyfService = notyfService;
			_reservationService = reservationService;
			_userService = userService;
		}

		public IActionResult Index(int page = 1)
		{
			var values = _reservationService.GetListWithDestinationAndUser().ToPagedList(page, 5);
			return View(values);
		}

		public IActionResult Detail(int id)
		{
			var values = _reservationService.GetReservationWithDestinationAndUser(id);
			return View(values);
		}

		public IActionResult ChangeStatus(int id)
		{
			_reservationService.ChangeStatus(id);
			_notyfService.Success("Durum güncellendi");
			return RedirectToAction("Index", "Reservation");
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			List<SelectListItem> destinationValues = (from x in _destinationService.GetList()
													  select new SelectListItem
													  {
														  Text = x.City,
														  Value = x.Id.ToString()
													  }).ToList();
			ViewBag.destinations = destinationValues;
			List<SelectListItem> userValues = (from x in await _userService.GetList()
											   select new SelectListItem
											   {
												   Text = x.NameSurname,
												   Value = x.Id.ToString()
											   }).ToList();
			ViewBag.users = userValues;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(Reservation reservation)
		{
			ReservationValidator reservationValidator = new();
			ValidationResult validationResult = reservationValidator.Validate(reservation);
			if (validationResult.IsValid)
			{
				reservation.ReservationStatus = "Onaylandı";
				reservation.Status = true;
				_reservationService.Add(reservation);
				_notyfService.Success("Rezervasyon eklendi");
				return RedirectToAction("Index", "Reservation");
			}
			else
			{
				List<SelectListItem> destinationValues = (from x in _destinationService.GetList()
														  select new SelectListItem
														  {
															  Text = x.City,
															  Value = x.Id.ToString()
														  }).ToList();
				ViewBag.destinations = destinationValues;
				List<SelectListItem> userValues = (from x in await _userService.GetList()
												   select new SelectListItem
												   {
													   Text = x.NameSurname,
													   Value = x.Id.ToString()
												   }).ToList();
				ViewBag.users = userValues;
				foreach (var item in validationResult.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
				return View();
			}
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			List<SelectListItem> destinationValues = (from x in _destinationService.GetList()
													  select new SelectListItem
													  {
														  Text = x.City,
														  Value = x.Id.ToString()
													  }).ToList();
			ViewBag.destinations = destinationValues;
			List<SelectListItem> userValues = (from x in await _userService.GetList()
											   select new SelectListItem
											   {
												   Text = x.NameSurname,
												   Value = x.Id.ToString()
											   }).ToList();
			ViewBag.users = userValues;
			var values = _reservationService.GetById(id);
			return View(values);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Reservation reservation)
		{
			ReservationValidator reservationValidator = new();
			ValidationResult validationResult = reservationValidator.Validate(reservation);
			if (validationResult.IsValid)
			{
				_reservationService.Update(reservation);
				_notyfService.Success("Rezervasyon güncellendi");
				return RedirectToAction("Index", "Reservation");
			}
			else
			{
				List<SelectListItem> destinationValues = (from x in _destinationService.GetList()
														  select new SelectListItem
														  {
															  Text = x.City,
															  Value = x.Id.ToString()
														  }).ToList();
				ViewBag.destinations = destinationValues;
				List<SelectListItem> userValues = (from x in await _userService.GetList()
												   select new SelectListItem
												   {
													   Text = x.NameSurname,
													   Value = x.Id.ToString()
												   }).ToList();
				ViewBag.users = userValues;
				foreach (var item in validationResult.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
				return View();
			}
		}

		public IActionResult Delete(int id)
		{
			var values = _reservationService.GetById(id);
			_reservationService.Delete(values);
			_notyfService.Success("Rezervasyon silindi");
			return RedirectToAction("Index", "Reservation");
		}

		public IActionResult ExcelDownload()
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			ExcelPackage excelPackage = new();
			var workSheet = excelPackage.Workbook.Worksheets.Add("Rezervasyon Listesi");
			workSheet.Cells["A1"].LoadFromCollection(_reservationService.GetListWithDestinationAndUser(), true, OfficeOpenXml.Table.TableStyles.Light10);
			workSheet.Cells[1, 1].Value = "No";
			workSheet.Cells[1, 2].Value = "Rezervasyonu Yapan";
			workSheet.Cells[1, 3].Value = "Rezervasyon Şehri";
			workSheet.Cells[1, 4].Value = "Gece Gün";
			workSheet.Cells[1, 5].Value = "Kişi Sayısı";
			workSheet.Cells[1, 6].Value = "Rezervasyon Tarihi";
			workSheet.Cells[1, 7].Value = "Rezervasyon Sonucu";
			workSheet.Cells[1, 8].Value = "Durum";
			int rowCount = 2, count = 1;
			foreach (var item in _reservationService.GetListWithDestinationAndUser())
			{
				workSheet.Cells[rowCount, 1].Value = count++;
				workSheet.Cells[rowCount, 2].Value = item.User.NameSurname;
				workSheet.Cells[rowCount, 3].Value = item.Destination.City;
				workSheet.Cells[rowCount, 4].Value = item.Description;
				workSheet.Cells[rowCount, 5].Value = item.PersonCount;
				workSheet.Cells[rowCount, 6].Value = item.Date.ToShortDateString();
				workSheet.Cells[rowCount, 7].Value = item.ReservationStatus;
				workSheet.Cells[rowCount, 8].Value = (item.Status == true ? "Aktif" : "Pasif");
				rowCount++;
			}
			var stream = new MemoryStream();
			excelPackage.SaveAs(stream);
			var content = stream.ToArray();
			return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheets", "Traversal | Rezervasyon Listesi.xlsx");
		}
	}
}