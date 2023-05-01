using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUILayer.ViewComponents.Destination
{
	public class SliderList : ViewComponent
	{
		private readonly IDestinationService _destinationService;

		public SliderList(IDestinationService destinationService)
		{
			_destinationService = destinationService;
		}

		public IViewComponentResult Invoke()
		{
			var values = _destinationService.GetListByActiveStatus();
			return View(values);
		}
	}
}