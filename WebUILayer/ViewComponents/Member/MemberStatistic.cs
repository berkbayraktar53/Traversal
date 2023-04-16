using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUILayer.ViewComponents.Member
{
	public class MemberStatistic : ViewComponent
	{
		private readonly ICommentService _commentService;
		private readonly IDestinationService _destinationService;
		private readonly IGuideService _guideService;
		private readonly INotificationService _notificationService;
		private readonly IReservationService _reservationService;
		private readonly IUserService _userService;

		public MemberStatistic(ICommentService commentService, IDestinationService destinationService, IGuideService guideService, INotificationService notificationService, IReservationService reservationService, IUserService userService)
		{
			_commentService = commentService;
			_destinationService = destinationService;
			_guideService = guideService;
			_notificationService = notificationService;
			_reservationService = reservationService;
			_userService = userService;
		}

		public IViewComponentResult Invoke()
		{
			ViewBag.totalCommentCount = _commentService.GetCommentListWithDestinationAndUserByActiveStatus().Count;
			ViewBag.totalDestinationCount = _destinationService.GetListByActiveStatus().Count;
			ViewBag.totalGuideCount = _guideService.GetListByActiveStatus().Count;
			ViewBag.totalNotificationCount = _notificationService.GetListByActiveStatus().Count;
			ViewBag.totalAcceptedReservationCount = _reservationService.GetListWithReservationByAccepted(_userService.GetById()).Count;
			ViewBag.totalPreviousReservationCount = _reservationService.GetListWithReservationByPrevious(_userService.GetById()).Count;
			return View();
		}
	}
}