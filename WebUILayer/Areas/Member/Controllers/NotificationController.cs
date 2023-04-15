using X.PagedList;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebUILayer.Areas.Member.Controllers
{
	[Area("Member")]
	[Authorize(Roles = "Member")]
	public class NotificationController : Controller
	{
		private readonly INotificationService _notificationService;

		public NotificationController(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		public IActionResult Index(int page = 1)
		{
			var values = _notificationService.GetListByActiveStatus().ToPagedList(page, 5);
			return View(values);
		}
	}
}