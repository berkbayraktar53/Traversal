using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUILayer.ViewComponents.Destination
{
    public class StatisticsList : ViewComponent
    {
        private readonly IClientService _clientService;
        private readonly IDestinationService _destinationService;
        private readonly IGuideService _guideService;

        public StatisticsList(IClientService clientService, IDestinationService destinationService, IGuideService guideService)
        {
            _clientService = clientService;
            _destinationService = destinationService;
            _guideService = guideService;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.clientCount = _clientService.GetListByActiveStatus().Count;
            ViewBag.destinationCount = _destinationService.GetListByActiveStatus().Count;
            ViewBag.guideCount = _guideService.GetListByActiveStatus().Count;
            return View();
        }
    }
}