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
            return View(values);
        }

        public IActionResult Detail(int id)
        {
            var values = _destinationService.GetById(id);
            return View(values);
        }

        public IActionResult ChangeStatus(int id)
        {
            _destinationService.ChangeStatus(id);
            _notyfService.Success("Durum güncellendi");
            return RedirectToAction("Index", "Destination");
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
                if (destinationListDto.Image != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(destinationListDto.Image.FileName);
                    var imageName = Guid.NewGuid() + extension;
                    var saveLocation = resource + "/wwwroot/images/destination/" + imageName;
                    var stream = new FileStream(saveLocation, FileMode.Create);
                    destinationListDto.Image.CopyToAsync(stream);
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
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var values = _destinationService.GetById(id);
            ViewBag.imageName = values.Image;
            return View(values);
        }

        [HttpPost]
        public IActionResult Edit(DestinationListDto destinationListDto)
        {
            DestinationValidator destinationValidator = new();
            ValidationResult validationResult = destinationValidator.Validate(destinationListDto);
            if (validationResult.IsValid)
            {
                Destination destination = new();
                if (destinationListDto.Image != null)
                {
                    var oldImage = _destinationService.GetById(destinationListDto.Id).Image;
                    string path = @"./images/destination/" + oldImage;
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(destinationListDto.Image.FileName);
                    var imageName = Guid.NewGuid() + extension;
                    var saveLocation = resource + "/wwwroot/images/destination/" + imageName;
                    var stream = new FileStream(saveLocation, FileMode.Create);
                    destinationListDto.Image.CopyToAsync(stream);
                    destination.Image = imageName;
                }
                destination.Id = destinationListDto.Id;
                destination.Capacity = destinationListDto.Capacity;
                destination.City = destinationListDto.City;
                destination.Date = DateTime.Now;
                destination.DayNight = destinationListDto.DayNight;
                destination.Description = destinationListDto.Description;
                destination.Price = destinationListDto.Price;
                destination.Status = true;
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
                _notyfService.Error("Rota güncellenemedi");
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            var values = _destinationService.GetById(id);
            _destinationService.Delete(values);
            _notyfService.Success("Rota silindi");
            return RedirectToAction("Index", "Destination");
        }
    }
}