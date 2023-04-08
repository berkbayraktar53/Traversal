using System;
using System.Linq;
using System.Threading;
using SignalR.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace SignalR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsController : ControllerBase
    {
        private readonly VisitorService _visitorService;

        public VisitorsController(VisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        [HttpGet("CreateVisitor")]
        public IActionResult CreateVisitor()
        {
            Random random = new();
            Enumerable.Range(1, 10).ToList().ForEach(p =>
            {
                foreach (ECity item in Enum.GetValues(typeof(ECity)))
                {
                    var newVisitor = new Visitor
                    {
                        City = item,
                        VisitCount = random.Next(100, 2000),
                        VisitDate = DateTime.Now.AddDays(p)
                    };
                    _visitorService.SaveVisitor(newVisitor).Wait();
                    Thread.Sleep(1000);
                }
            });
            return Ok("Ziyaretçiler başarılı bir şekilde eklendi");
        }
    }
}