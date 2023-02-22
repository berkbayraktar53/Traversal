using System;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessLayer.ValidationRules.FluentValidation;

namespace WebUILayer.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly INotyfService _notyfService;

        public ContactController(IContactService contactService, INotyfService notyfService)
        {
            _contactService = contactService;
            _notyfService = notyfService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Contact contact)
        {
            ContactValidator contactValidator = new();
            ValidationResult validationResult = contactValidator.Validate(contact);
            if (validationResult.IsValid)
            {
                contact.Date = DateTime.Now;
                contact.Status = true;
                _contactService.Add(contact);
                _notyfService.Success("Mesaj gönderildi");
                return RedirectToAction("Index", "Contact");
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
    }
}