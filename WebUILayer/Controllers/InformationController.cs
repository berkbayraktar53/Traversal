﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebUILayer.Controllers
{
	[AllowAnonymous]
	public class InformationController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}