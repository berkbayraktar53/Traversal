﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebUILayer.Areas.Member.Controllers
{
	[Area("Member")]
	[Authorize(Roles = "Member")]
	public class MessageController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}