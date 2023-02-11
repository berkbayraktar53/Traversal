using System.IO;
using System.Linq;
using ClosedXML.Excel;
using WebUILayer.Models;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebUILayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExcelController : Controller
    {
        private readonly IDestinationService _destinationService;

        public ExcelController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public List<DestinationExcelModel> DestinationList()
        {
            List<DestinationExcelModel> destinationExcelModels = new();
            destinationExcelModels = _destinationService.GetList().Select(p => new DestinationExcelModel
            {
                City = p.City,
                DayNight = p.DayNight,
                Price = p.Price,
                Capacity = p.Capacity
            }).ToList();
            return destinationExcelModels;
        }

        public IActionResult DestinationExcelReport()
        {
            var workBook = new XLWorkbook();
            var workSheet = workBook.Worksheets.Add("Tur Listesi");
            workSheet.Cell(1, 1).Value = "Şehir";
            workSheet.Cell(1, 2).Value = "Konaklama Süresi";
            workSheet.Cell(1, 3).Value = "Fiyat";
            workSheet.Cell(1, 4).Value = "Kapasite";
            int rowCount = 2;
            foreach (var item in DestinationList())
            {
                workSheet.Cell(rowCount, 1).Value = item.City;
                workSheet.Cell(rowCount, 2).Value = item.DayNight;
                workSheet.Cell(rowCount, 3).Value = item.Price;
                workSheet.Cell(rowCount, 4).Value = item.Capacity;
                rowCount++;
            }
            var stream = new MemoryStream();
            workBook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheets", "YeniListe.xlsx");
        }
    }
}