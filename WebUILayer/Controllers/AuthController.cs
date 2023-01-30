using EntityLayer.Dtos;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebUILayer.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        public AuthController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            var abcd = userLoginDto.Email;
            return View(abcd);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            if (ModelState.IsValid)
            {
                User user = new()
                {
                    NameSurname = userRegisterDto.NameSurname,
                    Email = userRegisterDto.Email,
                    Status = true
                };
                if (userRegisterDto.Password == userRegisterDto.ConfirmPassword)
                {
                    var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login", "Auth");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
            }
            return View(userRegisterDto);
        }
    }
}