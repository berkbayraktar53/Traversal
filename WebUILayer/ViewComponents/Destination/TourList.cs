using Microsoft.AspNetCore.Mvc;

namespace WebUILayer.ViewComponents.Destination
{
    public class TourList : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}