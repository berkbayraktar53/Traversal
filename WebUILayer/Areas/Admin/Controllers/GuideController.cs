using System;
using System.IO;
using X.PagedList;
using OfficeOpenXml;
using EntityLayer.Dtos;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessLayer.ValidationRules.FluentValidation;

namespace WebUILayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GuideController : Controller
    {
        private readonly IGuideService _guideService;
        private readonly IFileService _fileService;
        private readonly INotyfService _notyfService;

        public GuideController(IGuideService guideService, IFileService fileService, INotyfService notyfService)
        {
            _guideService = guideService;
            _fileService = fileService;
            _notyfService = notyfService;
        }

        public IActionResult Index(int page = 1)
        {
            var result = _guideService.GetList().ToPagedList(page, 5);
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
        public async Task<IActionResult> Add(GuideListDto guideListDto)
        {
            GuideValidator guideValidator = new();
            ValidationResult validationResult = guideValidator.Validate(guideListDto);
            if (validationResult.IsValid)
            {
                string folderName = "guide";
                Guide guide = new();
                var newImageName = await _fileService.FileSave(guideListDto.Image, "", folderName);
                guide.GuideImage = newImageName;
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
                NameSurname = result.NameSurname,
                Description = result.Description,
                InstagramLink = result.InstagramLink,
                TwitterLink = result.TwitterLink,
                Status = result.Status
            };
            return View(guideListDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GuideListDto guideListDto)
        {
            GuideValidator guideValidator = new();
            ValidationResult validationResult = guideValidator.Validate(guideListDto);
            if (validationResult.IsValid)
            {
                string folderName = "guide";
                Guide guide = new();
                var newImage = await _fileService.FileSave(guideListDto.Image, _guideService.GetById(guideListDto.Id).GuideImage, folderName);
                guide.Id = guideListDto.Id;
                guide.NameSurname = guideListDto.NameSurname;
                guide.GuideImage = newImage;
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
            string folderName = "guide";
            var result = _guideService.GetById(id);
            _guideService.Delete(result);
            _fileService.FileDelete(result.GuideImage, folderName);
            _notyfService.Success("Rehber silindi");
            return RedirectToAction("Index", "Guide");
        }

        public IActionResult ExcelDownload()
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
                workSheet.Cells[rowCount, 7].Value = (item.Status == true ? "Aktif" : "Pasif");
                rowCount++;
            }
            var stream = new MemoryStream();
            excelPackage.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheets", "Traversal | Rehber Listesi.xlsx");
        }
    }
}