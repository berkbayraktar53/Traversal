using Microsoft.AspNetCore.Mvc;

namespace WebUILayer.Areas.Member.Controllers
{
    [Area("Member")]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}