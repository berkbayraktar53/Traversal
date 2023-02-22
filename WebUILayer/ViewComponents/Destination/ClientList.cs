using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUILayer.ViewComponents.Destination
{
    public class ClientList : ViewComponent
    {
        private readonly IClientService _clientService;

        public ClientList(IClientService clientService)
        {
            _clientService = clientService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _clientService.GetListByActiveStatus();
            return View(result);
        }
    }
}