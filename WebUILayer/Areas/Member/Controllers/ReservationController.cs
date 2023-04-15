using System.Linq;
using X.PagedList;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessLayer.ValidationRules.FluentValidation;

namespace WebUILayer.Areas.Member.Controllers
{
	[Area("Member")]
	[Authorize(Roles = "Member")]
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

		public IActionResult MyCurrentReservation(int page = 1)
		{
			var values = _reservationService.GetListWithReservationByAccepted(_userService.GetById()).ToPagedList(page, 3);
			return View(values);
		}

		public IActionResult MyOldReservation(int page = 1)
		{
			var values = _reservationService.GetListWithReservationByPrevious(_userService.GetById()).ToPagedList(page, 3);
			return View(values);
		}

		[HttpGet]
		public IActionResult Add()
		{
			List<SelectListItem> values = (from x in _destinationService.GetListByActiveStatus()
										   select new SelectListItem
										   {
											   Text = x.City,
											   Value = x.Id.ToString()
										   }).ToList();
			ViewBag.destinationList = values;
			return View();
		}

		[HttpPost]
		public IActionResult Add(Reservation reservation)
		{
			ReservationValidator reservationValidator = new();
			ValidationResult validationResult = reservationValidator.Validate(reservation);
			if (validationResult.IsValid)
			{
				_reservationService.Add(reservation);
				_notyfService.Success("Rezervasyon eklendi");
				return RedirectToAction("MyCurrentReservation", "Reservation");
			}
			else
			{
				List<SelectListItem> values = (from x in _destinationService.GetListByActiveStatus()
											   select new SelectListItem
											   {
												   Text = x.City,
												   Value = x.Id.ToString()
											   }).ToList();
				ViewBag.destinationList = values;
				foreach (var item in validationResult.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
				_notyfService.Error("Rezervasyon eklenemedi");
				return View();
			}
		}
	}
}