using X.PagedList;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebUILayer.Controllers
{
    [AllowAnonymous]
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;

        public DestinationController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public IActionResult Index(int page = 1)
        {
            var result = _destinationService.GetListByActiveStatus().ToPagedList(page, 6);
            return View(result);
        }

        public IActionResult Detail(int id)
        {
            var result = _destinationService.GetById(id);
            return View(result);
        }
    }
}