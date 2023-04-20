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
	public class AboutController : Controller
	{
		private readonly IAboutService _aboutService;
		private readonly IFileService _fileService;
		private readonly INotyfService _notyfService;

		public AboutController(IAboutService aboutService, IFileService fileService, INotyfService notyfService)
		{
			_aboutService = aboutService;
			_fileService = fileService;
			_notyfService = notyfService;
		}

		public IActionResult Index(int page = 1)
		{
			var values = _aboutService.GetList().ToPagedList(page, 5);
			return View(values);
		}

		public IActionResult Detail(int id)
		{
			var values = _aboutService.GetById(id);
			return View(values);
		}

		public IActionResult ChangeStatus(int id)
		{
			_aboutService.ChangeStatus(id);
			_notyfService.Success("Durum güncellendi");
			return RedirectToAction("Index", "About");
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(AboutListDto aboutListDto)
		{
			AboutValidator destinationValidator = new();
			ValidationResult validationResult = destinationValidator.Validate(aboutListDto);
			if (validationResult.IsValid)
			{
				string folderName = "about";
				About about = new();
				var newImageName = await _fileService.FileSave(aboutListDto.Image, "", folderName);
				about.Image = newImageName;
				about.Title = aboutListDto.Title;
				about.Description = aboutListDto.Description;
				about.Status = true;
				_aboutService.Add(about);
				_notyfService.Success("Hakkımızda eklendi");
				return RedirectToAction("Index", "About");
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
			var about = _aboutService.GetById(id);
			AboutListDto aboutListDto = new()
			{
				Id = about.Id,
				Title = about.Title,
				Description = about.Description,
				Status = about.Status,
			};
			return View(aboutListDto);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(AboutListDto aboutListDto)
		{
			AboutValidator aboutValidator = new();
			ValidationResult validationResult = aboutValidator.Validate(aboutListDto);
			if (validationResult.IsValid)
			{
				string folderName = "about";
				About about = new();
				var newImage = await _fileService.FileSave(aboutListDto.Image, _aboutService.GetById(aboutListDto.Id).Image, folderName);
				about.Id = aboutListDto.Id;
				about.Image = newImage;
				about.Title = aboutListDto.Title;
				about.Description = aboutListDto.Description;
				about.Status = aboutListDto.Status;
				_aboutService.Update(about);
				_notyfService.Success("Hakkımızda güncellendi");
				return RedirectToAction("Index", "About");
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
			string folderName = "about";
			var about = _aboutService.GetById(id);
			_aboutService.Delete(about);
			_fileService.FileDelete(about.Image, folderName);
			_notyfService.Success("Hakkımızda silindi");
			return RedirectToAction("Index", "About");
		}

		public IActionResult ExcelDownload()
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			ExcelPackage excelPackage = new();
			var workSheet = excelPackage.Workbook.Worksheets.Add("Hakkımızda Listesi");
			workSheet.Cells["A1"].LoadFromCollection(_aboutService.GetList(), true, OfficeOpenXml.Table.TableStyles.Light10);
			workSheet.Cells[1, 1].Value = "No";
			workSheet.Cells[1, 2].Value = "Görsel";
			workSheet.Cells[1, 3].Value = "Başlık";
			workSheet.Cells[1, 4].Value = "Açıklama";
			workSheet.Cells[1, 5].Value = "Durum";
			int rowCount = 2, count = 1;
			foreach (var item in _aboutService.GetList())
			{
				workSheet.Cells[rowCount, 1].Value = count++;
				workSheet.Cells[rowCount, 2].Value = "/images/about/" + item.Image;
				workSheet.Cells[rowCount, 3].Value = item.Title;
				workSheet.Cells[rowCount, 4].Value = item.Description;
				workSheet.Cells[rowCount, 5].Value = (item.Status == true ? "Aktif" : "Pasif");
				rowCount++;
			}
			var stream = new MemoryStream();
			excelPackage.SaveAs(stream);
			var content = stream.ToArray();
			return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheets", "Traversal | Hakkımızda Listesi.xlsx");
		}
	}
}