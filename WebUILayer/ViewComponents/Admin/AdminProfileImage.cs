using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUILayer.ViewComponents.Admin
{
	public class AdminProfileImage : ViewComponent
	{
		private readonly IUserService _userService;

		public AdminProfileImage(IUserService userService)
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