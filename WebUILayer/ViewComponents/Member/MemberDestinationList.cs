using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUILayer.ViewComponents.Member
{
	public class MemberDestinationList : ViewComponent
	{
		private readonly IDestinationService _destinationService;

		public MemberDestinationList(IDestinationService destinationService)
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