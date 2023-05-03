using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using WebUILayer.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace WebUILayer.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class MailController : Controller
	{
		private readonly INotyfService _notyfService;

		public MailController(INotyfService notyfService)
		{
			_notyfService = notyfService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(MailRequest mailRequest)
		{
			MimeMessage mimeMessage = new();
			MailboxAddress mailboxAddressFrom = new("Traversal Rezervasyon Sitesi", "info@traversal.com");
			mimeMessage.From.Add(mailboxAddressFrom);
			MailboxAddress mailboxAddressTo = new(mailRequest.ReceiverMail, mailRequest.ReceiverMail);
			mimeMessage.To.Add(mailboxAddressTo);
			var bodyBuilder = new BodyBuilder
			{
				TextBody = mailRequest.Body
			};
			mimeMessage.Body = bodyBuilder.ToMessageBody();
			mimeMessage.Subject = mailRequest.Subject;
			SmtpClient smtpClient = new();
			smtpClient.Connect("smtp.gmail.com", 587, false);
			smtpClient.Authenticate("keremberk53@gmail.com", "dpjyhpoakydyjqwu");
			smtpClient.Send(mimeMessage);
			smtpClient.Disconnect(true);
			_notyfService.Success("Mail başarıyla gönderildi");
			return View();
		}
	}
}
