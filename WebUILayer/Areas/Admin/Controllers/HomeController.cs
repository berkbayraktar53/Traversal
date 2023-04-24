using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebUILayer.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class HomeController : Controller
	{
		private readonly IDestinationService _destinationService;
		private readonly IGuideService _guideService;
		private readonly IReservationService _reservationService;
		private readonly IUserService _userService;

		public HomeController(IDestinationService destinationService, IGuideService guideService, IReservationService reservationService, IUserService userService)
		{
			_destinationService = destinationService;
			_guideService = guideService;
			_reservationService = reservationService;
			_userService = userService;
		}

		public IActionResult Index()
		{
			ViewBag.totalDestinationCount = _destinationService.GetList().Count;
			ViewBag.totalGuideCount = _guideService.GetList().Count;
			ViewBag.totalReservationCount = _reservationService.GetList().Count;
			ViewBag.totalUserCount = _userService.GetList().Result.Count;
			ViewBag.totalActiveDestinationCount = _destinationService.GetListByActiveStatus().Count;
			ViewBag.totalActiveGuideCount = _guideService.GetListByActiveStatus().Count;
			ViewBag.totalActiveReservationCount = _reservationService.GetListWithDestinationByActiveStatus().Count;
			ViewBag.totalActiveUserCount = _userService.GetListByActiveStatus().Result.Count;
			return View();
		}
	}
}