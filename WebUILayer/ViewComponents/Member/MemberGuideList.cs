using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUILayer.ViewComponents.Member
{
	public class MemberGuideList : ViewComponent
	{
		private readonly IGuideService _guideService;

		public MemberGuideList(IGuideService guideService)
		{
			_guideService = guideService;
		}

		public IViewComponentResult Invoke()
		{
			var values = _guideService.GetFourGuideByActiveStatus();
			return View(values);
		}
	}
}