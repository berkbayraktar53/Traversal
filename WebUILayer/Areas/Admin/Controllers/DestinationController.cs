using System;
using System.IO;
using X.PagedList;
using EntityLayer.Dtos;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessLayer.ValidationRules.FluentValidation;
using OfficeOpenXml;

namespace WebUILayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;
        private readonly INotyfService _notyfService;

        public DestinationController(IDestinationService destinationService, INotyfService notyfService)
        {
            _destinationService = destinationService;
            _notyfService = notyfService;
        }

        public IActionResult Index(int page = 1)
        {
            var values = _destinationService.GetList().ToPagedList(page, 5);
            ViewBag.totalCount = _destinationService.GetList().Count;
            return View(values);
        }

        public IActionResult ChangeStatus(int id)
        {
            _destinationService.ChangeStatus(id);
            _notyfService.Success("Durum güncellendi");
            return RedirectToAction("Index", "Destination");
        }

        public IActionResult Detail(int id)
        {
            var values = _destinationService.GetById(id);
            return View(values);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(DestinationListDto destinationListDto)
        {
            DestinationValidator destinationValidator = new();
            ValidationResult validationResult = destinationValidator.Validate(destinationListDto);
            if (validationResult.IsValid)
            {
                Destination destination = new();
                if (destinationListDto.DestinationImage != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(destinationListDto.DestinationImage.FileName);
                    var imageName = Guid.NewGuid() + extension;
                    var saveLocation = resource + "/wwwroot/images/destination/" + imageName;
                    var stream = new FileStream(saveLocation, FileMode.Create);
                    destinationListDto.DestinationImage.CopyToAsync(stream);
                    destination.Image = imageName;
                }
                destination.Capacity = destinationListDto.Capacity;
                destination.City = destinationListDto.City;
                destination.Date = DateTime.Now;
                destination.DayNight = destinationListDto.DayNight;
                destination.Description = destinationListDto.Description;
                destination.Price = destinationListDto.Price;
                destination.Status = true;
                _destinationService.Add(destination);
                _notyfService.Success("Rota eklendi");
                return RedirectToAction("Index", "Destination");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _destinationService.GetById(id);
            DestinationListDto destinationListDto = new()
            {
                Id = result.Id,
                Image = result.Image,
                City = result.City,
                Description = result.Description,
                DayNight = result.DayNight,
                Capacity = result.Capacity,
                Price = result.Price,
                Date = result.Date,
                Status = result.Status,
            };
            return View(destinationListDto);
        }

        [HttpPost]
        public IActionResult Edit(DestinationListDto destinationListDto)
        {
            DestinationValidator destinationValidator = new();
            ValidationResult validationResult = destinationValidator.Validate(destinationListDto);
            if (validationResult.IsValid)
            {
                Destination destination = new();
                if (destinationListDto.DestinationImage != null)
                {
                    var oldImage = _destinationService.GetById(destinationListDto.Id).Image;
                    var path = Directory.GetCurrentDirectory() + "/wwwroot/images/destination/" + oldImage;
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(destinationListDto.DestinationImage.FileName);
                    var imageName = Guid.NewGuid() + extension;
                    var saveLocation = resource + "/wwwroot/images/destination/" + imageName;
                    var stream = new FileStream(saveLocation, FileMode.Create);
                    destinationListDto.DestinationImage.CopyToAsync(stream);
                    destination.Image = imageName;
                }
                destination.Id = destinationListDto.Id;
                destination.Capacity = destinationListDto.Capacity;
                destination.City = destinationListDto.City;
                destination.Date = destinationListDto.Date;
                destination.DayNight = destinationListDto.DayNight;
                destination.Description = destinationListDto.Description;
                destination.Price = destinationListDto.Price;
                destination.Status = destinationListDto.Status;
                _destinationService.Update(destination);
                _notyfService.Success("Rota güncellendi");
                return RedirectToAction("Index", "Destination");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            var result = _destinationService.GetById(id);
            var path = Directory.GetCurrentDirectory() + "/wwwroot/images/destination/" + result.Image;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _destinationService.Delete(result);
            _notyfService.Success("Rota silindi");
            return RedirectToAction("Index", "Destination");
        }

        public IActionResult DestinationExcelList()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new();
            var workSheet = excelPackage.Workbook.Worksheets.Add("Rota Listesi");
            workSheet.Cells["A1"].LoadFromCollection(_destinationService.GetList(), true, OfficeOpenXml.Table.TableStyles.Light10);
            workSheet.Cells[1, 1].Value = "No";
            workSheet.Cells[1, 2].Value = "Resim";
            workSheet.Cells[1, 3].Value = "Şehir";
            workSheet.Cells[1, 4].Value = "Açıklama";
            workSheet.Cells[1, 5].Value = "Gece Gündüz";
            workSheet.Cells[1, 6].Value = "Kapasite";
            workSheet.Cells[1, 7].Value = "Fiyat";
            workSheet.Cells[1, 8].Value = "Tarih";
            workSheet.Cells[1, 9].Value = "Durum";
            int rowCount = 2, count = 1;
            foreach (var item in _destinationService.GetList())
            {
                workSheet.Cells[rowCount, 1].Value = count++;
                workSheet.Cells[rowCount, 2].Value = "/images/destination/" + item.Image;
                workSheet.Cells[rowCount, 3].Value = item.City;
                workSheet.Cells[rowCount, 4].Value = item.Description;
                workSheet.Cells[rowCount, 5].Value = item.DayNight;
                workSheet.Cells[rowCount, 6].Value = item.Capacity;
                workSheet.Cells[rowCount, 7].Value = item.Price;
                workSheet.Cells[rowCount, 8].Value = item.Date.ToShortDateString();
                workSheet.Cells[rowCount, 9].Value = (item.Status == true ? "AKTİF" : "PASİF");
                rowCount++;
            }
            var stream = new MemoryStream();
            excelPackage.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheets", "RotaListesi.xlsx");
        }
    }
}