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
	public class ClientController : Controller
	{
		private readonly IClientService _clientService;
		private readonly IFileService _fileService;
		private readonly INotyfService _notyfService;

		public ClientController(IClientService clientService, IFileService fileService, INotyfService notyfService)
		{
			_clientService = clientService;
			_fileService = fileService;
			_notyfService = notyfService;
		}

		public IActionResult Index(int page = 1)
		{
			var values = _clientService.GetList().ToPagedList(page, 5);
			return View(values);
		}

		public IActionResult ChangeStatus(int id)
		{
			_clientService.ChangeStatus(id);
			_notyfService.Success("Durum güncellendi");
			return RedirectToAction("Index", "Client");
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(ClientListDto clientListDto)
		{
			ClientValidator clientValidator = new();
			ValidationResult validationResult = clientValidator.Validate(clientListDto);
			if (validationResult.IsValid)
			{
				string folderName = "client";
				Client client = new();
				var newImageName = await _fileService.FileSave(clientListDto.ClientImage, "", folderName);
				client.ClientImage = newImageName;
				client.NameSurname = clientListDto.NameSurname;
				client.ClientComment = clientListDto.ClientComment;
				client.Status = true;
				_clientService.Add(client);
				_notyfService.Success("Müşteri eklendi");
				return RedirectToAction("Index", "Client");
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
			var client = _clientService.GetById(id);
			ClientListDto clientListDto = new()
			{
				Id = client.Id,
				NameSurname = client.NameSurname,
				ClientComment = client.ClientComment,
				Status = client.Status,
			};
			return View(clientListDto);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(ClientListDto clientListDto)
		{
			ClientValidator clientValidator = new();
			ValidationResult validationResult = clientValidator.Validate(clientListDto);
			if (validationResult.IsValid)
			{
				string folderName = "client";
				Client client = new();
				var newImage = await _fileService.FileSave(clientListDto.ClientImage, _clientService.GetById(clientListDto.Id).ClientImage, folderName);
				client.Id = clientListDto.Id;
				client.ClientImage = newImage;
				client.NameSurname = clientListDto.NameSurname;
				client.ClientComment = clientListDto.ClientComment;
				client.Status = clientListDto.Status;
				_clientService.Update(client);
				_notyfService.Success("Müşteri güncellendi");
				return RedirectToAction("Index", "Client");
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
			string folderName = "client";
			var client = _clientService.GetById(id);
			_clientService.Delete(client);
			_fileService.FileDelete(client.ClientImage, folderName);
			_notyfService.Success("Müşteri silindi");
			return RedirectToAction("Index", "Client");
		}

		public IActionResult ExcelDownload()
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			ExcelPackage excelPackage = new();
			var workSheet = excelPackage.Workbook.Worksheets.Add("Müşteri Listesi");
			workSheet.Cells["A1"].LoadFromCollection(_clientService.GetList(), true, OfficeOpenXml.Table.TableStyles.Light10);
			workSheet.Cells[1, 1].Value = "No";
			workSheet.Cells[1, 2].Value = "Görsel";
			workSheet.Cells[1, 3].Value = "Müşteri Adı Soyadı";
			workSheet.Cells[1, 4].Value = "Müşteri Yorumu";
			workSheet.Cells[1, 5].Value = "Durum";
			int rowCount = 2, count = 1;
			foreach (var item in _clientService.GetList())
			{
				workSheet.Cells[rowCount, 1].Value = count++;
				workSheet.Cells[rowCount, 2].Value = "/images/client/" + item.ClientImage;
				workSheet.Cells[rowCount, 3].Value = item.NameSurname;
				workSheet.Cells[rowCount, 4].Value = item.ClientComment;
				workSheet.Cells[rowCount, 5].Value = (item.Status == true ? "Aktif" : "Pasif");
				rowCount++;
			}
			var stream = new MemoryStream();
			excelPackage.SaveAs(stream);
			var content = stream.ToArray();
			return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheets", "Traversal | Müşteri Listesi.xlsx");
		}
	}
}