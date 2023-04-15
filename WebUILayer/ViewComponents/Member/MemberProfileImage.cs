using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUILayer.ViewComponents.Member
{
	public class MemberProfileImage : ViewComponent
	{
		private readonly IUserService _userService;

		public MemberProfileImage(IUserService userService)
		{
			_userService = userService;
		}

		public IViewComponentResult Invoke()
		{
			var values = _userService.GetByUser();
			return View(values);
		}
	}
}