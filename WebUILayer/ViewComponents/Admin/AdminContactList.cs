using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUILayer.ViewComponents.Admin
{
	public class AdminContactList : ViewComponent
	{
		private readonly IContactService _contactService;

		public AdminContactList(IContactService contactService)
		{
			_contactService = contactService;
		}

		public IViewComponentResult Invoke()
		{
			var values = _contactService.GetFourContactByActiveStatus();
			return View(values);
		}
	}
}