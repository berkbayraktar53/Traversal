using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUILayer.ViewComponents.Admin
{
	public class AdminReservationList : ViewComponent
	{
		private readonly IReservationService _reservationService;

		public AdminReservationList(IReservationService reservationService)
		{
			_reservationService = reservationService;
		}

		public IViewComponentResult Invoke()
		{
			var values = _reservationService.GetFourReservationByActiveStatus();
			return View(values);
		}
	}
}