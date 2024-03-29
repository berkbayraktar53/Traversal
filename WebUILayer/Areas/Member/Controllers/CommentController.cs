﻿using X.PagedList;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebUILayer.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult Index(int page = 1)
        {
            var values = _commentService.GetCommentListWithDestinationAndUserByActiveStatus().ToPagedList(page, 2);
            return View(values);
        }
    }
}