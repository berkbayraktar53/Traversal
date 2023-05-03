using MimeKit;
using MailKit.Net.Smtp;
using WebUILayer.Models;
using EntityLayer.Concrete;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace WebUILayer.Controllers
{
	[AllowAnonymous]
	public class PasswordChangeController : Controller
	{
		private readonly INotyfService _notyfService;
		private readonly UserManager<User> _userManager;

		public PasswordChangeController(INotyfService notyfService, UserManager<User> userManager)
		{
			_notyfService = notyfService;
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgetPasswordViewModel)
		{
			var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Email);
			if (user == null)
			{
				_notyfService.Error("Girdiğiniz email adresi sistemde kayıtlı değil");
				return RedirectToAction("ForgotPassword", "PasswordChange");
			}
			string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
			var passwordResetTokenLink = Url.Action("ResetPassword", "PasswordChange", new
			{
				userId = user.Id,
				token = passwordResetToken
			}, HttpContext.Request.Scheme);
			MimeMessage mimeMessage = new();
			MailboxAddress mailboxAddressFrom = new("Traversal Rezervasyon Sitesi", "info@traversal.com");
			mimeMessage.From.Add(mailboxAddressFrom);
			MailboxAddress mailboxAddressTo = new("User", forgetPasswordViewModel.Email);
			mimeMessage.To.Add(mailboxAddressTo);
			var bodyBuilder = new BodyBuilder
			{
				TextBody = passwordResetTokenLink
			};
			mimeMessage.Body = bodyBuilder.ToMessageBody();
			mimeMessage.Subject = "Şifre Değişiklik Talebi";
			SmtpClient client = new();
			client.Connect("smtp.gmail.com", 587, false);
			client.Authenticate("keremberk53@gmail.com", "dpjyhpoakydyjqwu");
			client.Send(mimeMessage);
			client.Disconnect(true);
			_notyfService.Success("Şifre yenileme bağlantısı mail adresinize gönderildi");
			return View();
		}

		[HttpGet]
		public IActionResult ResetPassword(string userId, string token)
		{
			TempData["userId"] = userId;
			TempData["token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
		{
			if (resetPasswordViewModel.Password != resetPasswordViewModel.ConfirmPassword)
			{
				_notyfService.Error("Şifreler birbiriyle eşleşmiyor");
				return RedirectToAction("ResetPassword", "PasswordChange");

			}
			var userId = TempData["userId"];
			var token = TempData["token"];
			if (userId == null || token == null)
			{
				if (userId == null)
				{
					_notyfService.Error("Kullanıcı adı bulunamadı");
				}
				else
				{
					_notyfService.Error("Token bulunamadı");
				}
			}
			var user = await _userManager.FindByIdAsync(userId.ToString());
			var result = await _userManager.ResetPasswordAsync(user, token.ToString(), resetPasswordViewModel.Password);
			if (result.Succeeded)
			{
				_notyfService.Success("Şifreniz başarıyla değiştirildi");
				return RedirectToAction("Login", "Auth");
			}
			return View();
		}
	}
}