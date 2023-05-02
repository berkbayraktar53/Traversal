using System;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace WebUILayer.Controllers
{
	[AllowAnonymous]
	public class CommentController : Controller
	{
		private readonly ICommentService _commentService;
		private readonly INotyfService _notyfService;
		private readonly IUserService _userService;

		public CommentController(ICommentService commentService, INotyfService notyfService, IUserService userService)
		{
			_commentService = commentService;
			_notyfService = notyfService;
			_userService = userService;
		}

		[HttpGet]
		public PartialViewResult Add()
		{
			return PartialView();
		}

		[HttpPost]
		public IActionResult Add(Comment comment)
		{
			comment.UserId = _userService.GetByUser().Id;
			comment.UserImage = _userService.GetByUser().UserImage;
			comment.NameSurname = _userService.GetByUser().NameSurname;
			comment.CommentDate = DateTime.Now;
			comment.Status = true;
			_notyfService.Success("Yorum eklendi");
			_commentService.Add(comment);
			return RedirectToAction("Index", "Destination");
		}
	}
}