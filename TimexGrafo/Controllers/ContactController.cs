using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Routing;
using TimexGrafo.ViewModels;
using TimexGrafo.Models;

namespace TimexGrafo.Controllers
{
    [Route("/Kontakt")]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ActiveMenu = "nav-link-kontakt";
            var viewModel = new ContactMailViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [Route("SendMail")]
        public IActionResult SendMail(ContactMailViewModel viewModel)
        {
            try
            {
                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("TimexGrafo Kontakt",
                "timexgrafomailer@gmail.com");
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress("Timex Grafo Office",
                "office@timexgrafo.rs");
                message.To.Add(to);

                message.Subject = "Nova Kontakt Poruka";

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<h1>Kontakt poruka</h1><br/>" +
                    "<table>" +
                    "<tbody>" +
                    "<tr><th>Ime</th><td style='width: 200px'>" + viewModel.Name + "</td></tr>" +
                    "<tr><th>Prezime</th><td style='width: 200px'>" + viewModel.LastName + "</td></tr>" +
                    "<tr><th>Email</th><td style='width: 200px'>" + viewModel.Email + "</td></tr></tbody></table>" +
                    "<h3>Poruka:</h3><p style='word-break: break-all;width: 300px;'>" + viewModel.Message + "</p>";

                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                client.Connect("Smtp.gmail.com", 465, true);
                client.Authenticate("timexgrafomailer@gmail.com", "000000");

                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Route("SendErrorMail")]
        public IActionResult SendErrorMail(ErrorViewModel viewModel)
        {
            try
            {
                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("TimexGrafo Kontakt Za Probleme Na Sajtu",
                "timexgrafomailer@gmail.com");
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress("TimexGrafo Office",
                "office@timexgrafo.rs");
                message.To.Add(to);

                message.Subject = "Nova kontakt poruka o problemu na sajtu";

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<h1>Kontakt poruka o problemu na sajtu</h1><br/>" +
                    "<table>" +
                    "<tbody>" +
                    "<tr><th>Ime</th><td style='width: 200px'>" + viewModel.Name + "</td></tr>" +
                    "<tr><th>Prezime</th><td style='width: 200px'>" + viewModel.LastName + "</td></tr>" +
                    "<tr><th>Email</th><td style='width: 200px'>" + viewModel.Email + "</td></tr></tbody></table>" +
                    "<h3>Poruka:</h3><p style='word-break: break-all;width: 300px;'>" + viewModel.Message + "</p>";
                //bodyBuilder.TextBody = "Hello World!";

                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                client.Connect("Smtp.gmail.com", 465, true);
                client.Authenticate("timexgrafomailer@gmail.com", "000000");

                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return RedirectToAction("Index","Home");
        }
    }
}