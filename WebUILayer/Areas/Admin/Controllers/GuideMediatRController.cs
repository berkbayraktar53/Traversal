using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebUILayer.CQRS.Queries.GuideQueries;
using WebUILayer.CQRS.Commands.GuideCommands;

namespace WebUILayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GuideMediatRController : Controller
    {
        private readonly IMediator _mediator;

        public GuideMediatRController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _mediator.Send(new GetAllGuideQuery());
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> GetGuide(int id)
        {
            var values = await _mediator.Send(new GetGuideByIdQuery(id));
            return View(values);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateGuideCommand createGuideCommand)
        {
            await _mediator.Send(createGuideCommand);
            return RedirectToAction("Index", "GuideMediatR");
        }
    }
}