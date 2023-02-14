using System;
using System.IO;
using X.PagedList;
using OfficeOpenXml;
using EntityLayer.Dtos;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessLayer.ValidationRules.FluentValidation;

namespace WebUILayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GuideController : Controller
    {
        private readonly IGuideService _guideService;
        private readonly INotyfService _notyfService;

        public GuideController(IGuideService guideService, INotyfService notyfService)
        {
            _guideService = guideService;
            _notyfService = notyfService;
        }

        public IActionResult Index(int page = 1)
        {
            var result = _guideService.GetList().ToPagedList(page, 5);
            ViewBag.totalCount = _guideService.GetList().Count;
            return View(result);
        }

        public IActionResult ChangeStatus(int id)
        {
            _guideService.ChangeStatus(id);
            _notyfService.Success("Durum güncellendi");
            return RedirectToAction("Index", "Guide");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(GuideListDto guideListDto)
        {
            GuideValidator guideValidator = new();
            ValidationResult validationResult = guideValidator.Validate(guideListDto);
            if (validationResult.IsValid)
            {
                Guide guide = new();
                if (guideListDto.Image != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(guideListDto.Image.FileName);
                    var imageName = Guid.NewGuid() + extension;
                    var saveLocation = resource + "/wwwroot/images/guide/" + imageName;
                    var stream = new FileStream(saveLocation, FileMode.Create);
                    guideListDto.Image.CopyToAsync(stream);
                    guide.GuideImage = imageName;
                }
                guide.NameSurname = guideListDto.NameSurname;
                guide.Description = guideListDto.Description;
                guide.InstagramLink = guideListDto.InstagramLink;
                guide.TwitterLink = guideListDto.TwitterLink;
                guide.Status = true;
                _guideService.Add(guide);
                _notyfService.Success("Rehber eklendi");
                return RedirectToAction("Index", "Guide");
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
            var result = _guideService.GetById(id);
            GuideListDto guideListDto = new()
            {
                Id = result.Id,
                GuideImage = result.GuideImage,
                NameSurname = result.NameSurname,
                Description = result.Description,
                InstagramLink = result.InstagramLink,
                TwitterLink = result.TwitterLink,
                Status = result.Status
            };
            return View(guideListDto);
        }

        [HttpPost]
        public IActionResult Edit(GuideListDto guideListDto)
        {
            GuideValidator guideValidator = new();
            ValidationResult validationResult = guideValidator.Validate(guideListDto);
            if (validationResult.IsValid)
            {
                Guide guide = new();
                if (guideListDto.Image != null)
                {
                    var oldImage = _guideService.GetById(guideListDto.Id).GuideImage;
                    var path = Directory.GetCurrentDirectory() + "/wwwroot/images/guide/" + oldImage;
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(guideListDto.Image.FileName);
                    var imageName = Guid.NewGuid() + extension;
                    var saveLocation = resource + "/wwwroot/images/guide/" + imageName;
                    var stream = new FileStream(saveLocation, FileMode.Create);
                    guideListDto.Image.CopyToAsync(stream);
                    guide.GuideImage = imageName;
                }
                guide.Id = guideListDto.Id;
                guide.NameSurname = guideListDto.NameSurname;
                guide.Description = guideListDto.Description;
                guide.InstagramLink = guideListDto.InstagramLink;
                guide.TwitterLink = guideListDto.TwitterLink;
                guide.Status = guideListDto.Status;
                _guideService.Update(guide);
                _notyfService.Success("Rehber güncellendi");
                return RedirectToAction("Index", "Guide");
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
            var result = _guideService.GetById(id);
            var path = Directory.GetCurrentDirectory() + "/wwwroot/images/guide/" + result.GuideImage;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _guideService.Delete(result);
            _notyfService.Success("Rehber silindi");
            return RedirectToAction("Index", "Guide");
        }

        public IActionResult GuideExcelList()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new();
            var workSheet = excelPackage.Workbook.Worksheets.Add("Rehber Listesi");
            workSheet.Cells["A1"].LoadFromCollection(_guideService.GetList(), true, OfficeOpenXml.Table.TableStyles.Light10);
            workSheet.Cells[1, 1].Value = "No";
            workSheet.Cells[1, 2].Value = "Resim";
            workSheet.Cells[1, 3].Value = "Ad Soyad";
            workSheet.Cells[1, 4].Value = "Açıklama";
            workSheet.Cells[1, 5].Value = "Instagram Link";
            workSheet.Cells[1, 6].Value = "Twitter Link";
            workSheet.Cells[1, 7].Value = "Durum";
            int rowCount = 2, count = 1;
            foreach (var item in _guideService.GetList())
            {
                workSheet.Cells[rowCount, 1].Value = count++;
                workSheet.Cells[rowCount, 2].Value = "/images/guide/" + item.GuideImage;
                workSheet.Cells[rowCount, 3].Value = item.NameSurname;
                workSheet.Cells[rowCount, 4].Value = item.Description;
                workSheet.Cells[rowCount, 5].Value = item.InstagramLink;
                workSheet.Cells[rowCount, 6].Value = item.TwitterLink;
                workSheet.Cells[rowCount, 7].Value = (item.Status == true ? "AKTİF" : "PASİF");
                rowCount++;
            }
            var stream = new MemoryStream();
            excelPackage.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheets", "RehberListesi.xlsx");
        }
    }
}