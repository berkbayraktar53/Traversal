using Microsoft.AspNetCore.Mvc;

namespace WebUILayer.Areas.Member.Controllers
{
    [Area("Member")]
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}