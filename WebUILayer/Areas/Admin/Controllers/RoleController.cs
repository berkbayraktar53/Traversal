using X.PagedList;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessLayer.ValidationRules.FluentValidation;
using OfficeOpenXml;
using System.IO;

namespace WebUILayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly IRoleService _roleService;

        public RoleController(INotyfService notyfService, IRoleService roleService)
        {
            _notyfService = notyfService;
            _roleService = roleService;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.totalRoleCount = _roleService.GetList().Result.Count;
            var values = _roleService.GetList().Result.ToPagedList(page, 5);
            return View(values);
        }

        public async Task<IActionResult> ChangeStatus(int id)
        {
            await _roleService.ChangeStatus(id);
            _notyfService.Success("Durum Değiştirildi");
            return RedirectToAction("Index", "Role");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Role role)
        {
            RoleValidator roleValidator = new();
            ValidationResult validationResult = roleValidator.Validate(role);
            if (validationResult.IsValid)
            {
                await _roleService.Add(role);
                _notyfService.Success("Rol Eklendi");
                return RedirectToAction("Index", "Role");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                _notyfService.Error("Rol Eklenemedi");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var values = await _roleService.GetById(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Role role)
        {
            RoleValidator roleValidator = new();
            ValidationResult validationResult = roleValidator.Validate(role);
            if (validationResult.IsValid)
            {
                await _roleService.Update(role);
                _notyfService.Success("Rol Güncellendi");
                return RedirectToAction("Index", "Role");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                _notyfService.Error("Rol Güncellenemedi");
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                var values = await _roleService.GetById(id);
                await _roleService.Delete(values);
                _notyfService.Success("Rol Silindi");
                return RedirectToAction("Index", "Role");
            }
            else
            {
                _notyfService.Error("Rol Silinemedi");
                return View();
            }
        }

        public async Task<IActionResult> RoleExcelList()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new();
            var workSheet = excelPackage.Workbook.Worksheets.Add("Rol Listesi");
            workSheet.Cells["A1"].LoadFromCollection(await _roleService.GetList(), true, OfficeOpenXml.Table.TableStyles.Light10);
            workSheet.Cells[1, 1].Value = "No";
            workSheet.Cells[1, 2].Value = "Rol Adı";
            workSheet.Cells[1, 3].Value = "Rol Açıklaması";
            workSheet.Cells[1, 4].Value = "Durum";
            int rowCount = 2, count = 1;
            foreach (var role in await _roleService.GetList())
            {
                workSheet.Cells[rowCount, 1].Value = count++;
                workSheet.Cells[rowCount, 2].Value = role.Name;
                workSheet.Cells[rowCount, 3].Value = role.Description;
                workSheet.Cells[rowCount, 4].Value = (role.Status == true ? "AKTİF" : "PASİF");
                rowCount++;
            }
            var stream = new MemoryStream();
            excelPackage.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheets", "Traversal | Rol Listesi.xlsx");
        }
    }
}