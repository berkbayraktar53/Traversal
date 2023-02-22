using Microsoft.AspNetCore.Mvc;

namespace WebUILayer.ViewComponents.Destination
{
    public class SliderList : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}