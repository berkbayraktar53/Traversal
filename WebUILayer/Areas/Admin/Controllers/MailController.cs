using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using WebUILayer.Areas.Admin.Models;

namespace WebUILayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(MailRequest mailRequest)
        {
            MimeMessage mimeMessage = new();
            MailboxAddress mailboxAddressFrom = new("Traversal Rezervasyon Sitesi", "keremberk53@gmail.com");
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
            smtpClient.Authenticate("keremberk53@gmail.com", "mqhxbxvdvlatmzbm");
            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);

            return View();
        }
    }
}
