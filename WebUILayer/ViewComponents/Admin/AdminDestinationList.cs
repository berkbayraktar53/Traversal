using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUILayer.ViewComponents.Admin
{
	public class AdminDestinationList : ViewComponent
	{
		private readonly IDestinationService _destinationService;

		public AdminDestinationList(IDestinationService destinationService)
		{
			_destinationService = destinationService;
		}

		public IViewComponentResult Invoke()
		{
			var values = _destinationService.GetFourDestinationByActiveStatus();
			return View(values);
		}
	}
}