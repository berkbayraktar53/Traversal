using System.Linq;
using X.PagedList;
using System.Globalization;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebUILayer.Areas.Member.Controllers
{
	[Area("Member")]
	[Authorize(Roles = "Member")]
	public class DestinationController : Controller
	{
		private readonly IDestinationService _destinationService;

		public DestinationController(IDestinationService destinationService)
		{
			_destinationService = destinationService;
		}

		public IActionResult Index(int page = 1)
		{
			var values = _destinationService.GetListByActiveStatus().ToPagedList(page, 4);
			return View(values);
		}

		public IActionResult GetCitiesSearchByName(string searchString)
		{
			ViewBag.searchString = searchString;
			var getCityName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(searchString.ToLower());
			var getDescriptionName = searchString.ToLower();
			var values = _destinationService.GetListByActiveStatus();
			if (!string.IsNullOrEmpty(searchString))
			{
				values = values.Where(y => y.City.Contains(getCityName) || y.Description.Contains(getDescriptionName)).ToList();
			}
			return View(values.ToPagedList(1, 4));
		}
	}
}