using X.PagedList;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using System.Linq;

namespace WebUILayer.Controllers
{
    [AllowAnonymous]
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;

        public DestinationController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public IActionResult Index(int page = 1)
        {
            var result = _destinationService.GetListByActiveStatus().ToPagedList(page, 6);
            return View(result);
        }

        public IActionResult Detail(int id)
        {
            var result = _destinationService.GetById(id);
            return View(result);
        }

		public IActionResult GetCitiesSearchByName(string cityName)
		{
			ViewBag.cityName = cityName;
			var getCityName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cityName.ToLower());
			var getDescriptionName = cityName.ToLower();
			var values = _destinationService.GetListByActiveStatus();
			if (!string.IsNullOrEmpty(cityName))
			{
				values = values.Where(y => y.City.Contains(getCityName)).ToList();
			}
			return View(values.ToPagedList(1, 5));
		}
	}
}