using System;
using System.IO;
using EntityLayer.Concrete;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebUILayer.Areas.Member.Models;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace WebUILayer.Areas.Member.Controllers
{
    [Area("Member")]
    public class ProfileController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly UserManager<User> _userManager;

        public ProfileController(INotyfService notyfService, UserManager<User> userManager)
        {
            _notyfService = notyfService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userValues = await _userManager.FindByNameAsync(User.Identity.Name);
            MemberEditViewModel memberEditViewModel = new()
            {
                NameSurname = userValues.NameSurname,
                AboutUser = userValues.AboutUser,
                UserName = userValues.UserName,
                Email = userValues.Email
            };
            return View(memberEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(MemberEditViewModel memberEditViewModel)
        {
            var userValues = await _userManager.FindByNameAsync(User.Identity.Name);
            if (memberEditViewModel.UserImage != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(memberEditViewModel.UserImage.FileName);
                var imageName = Guid.NewGuid() + extension;
                var saveLocation = resource + "/wwwroot/memberimages/" + imageName;
                var stream = new FileStream(saveLocation, FileMode.Create);
                await memberEditViewModel.UserImage.CopyToAsync(stream);
                userValues.UserImage = imageName;
            }
            userValues.NameSurname = memberEditViewModel.NameSurname;
            userValues.AboutUser = memberEditViewModel.AboutUser;
            userValues.UserName = memberEditViewModel.UserName;
            userValues.Email = memberEditViewModel.Email;
            userValues.PasswordHash = _userManager.PasswordHasher.HashPassword(userValues, memberEditViewModel.Password);
            var result = await _userManager.UpdateAsync(userValues);
            if (result.Succeeded)
            {
                _notyfService.Success("Profil güncellendi");
                return RedirectToAction("Index", "Profile");
            }
            _notyfService.Error("Profil güncellenemedi");
            return View();
        }
    }
}