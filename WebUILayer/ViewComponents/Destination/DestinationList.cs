using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUILayer.ViewComponents.Destination
{
    public class DestinationList : ViewComponent
    {
        private readonly IDestinationService _destinationService;

        public DestinationList(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _destinationService.GetEightDestinationByActiveStatus();
            return View(result);
        }
    }
}