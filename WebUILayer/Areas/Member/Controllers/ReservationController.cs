using System.Linq;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace WebUILayer.Areas.Member.Controllers
{
    [Area("Member")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly UserManager<User> _userManager;

        public ReservationController(IReservationService reservationService, UserManager<User> userManager)
        {
            _reservationService = reservationService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewBag.userNameSurname = _userManager.FindByNameAsync(User.Identity.Name).Result.NameSurname;
            var values = _reservationService.GetListWithDestination().OrderByDescending(p => p.Date).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult Add()
        {
            //List<SelectListItem> cityList = (from destination in _destinationService.GetListByActiveStatus()
            //                                 select new SelectListItem
            //                                 {
            //                                     Text = destination.City,
            //                                     Value = destination.Id.ToString()
            //                                 }).ToList();
            //ViewBag.cityList = cityList;
            return View();
        }
    }
}