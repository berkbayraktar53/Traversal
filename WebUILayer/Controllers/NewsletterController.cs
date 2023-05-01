using System;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace WebUILayer.Controllers
{
	[AllowAnonymous]
	public class NewsletterController : Controller
	{
		private readonly INewsletterService _newsletterService;
		private readonly INotyfService _notyfService;

		public NewsletterController(INewsletterService newsletterService, INotyfService notyfService)
		{
			_newsletterService = newsletterService;
			_notyfService = notyfService;
		}

		[HttpPost]
		public IActionResult Add(string email)
		{
			Newsletter newsletter = new()
			{
				Email = email,
				Date = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
				Status = true
			};
			_newsletterService.Add(newsletter);
			_notyfService.Success("Abone olundu");
			return RedirectToAction("Index", "Home");
		}
	}
}