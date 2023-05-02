using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUILayer.ViewComponents.Comment
{
	public class CommentList : ViewComponent
	{
		private readonly ICommentService _commentService;

		public CommentList(ICommentService commentService)
		{
			_commentService = commentService;
		}

		public IViewComponentResult Invoke(int id)
		{
			var values = _commentService.GetListByDestination(id);
			return View(values);
		}
	}
}