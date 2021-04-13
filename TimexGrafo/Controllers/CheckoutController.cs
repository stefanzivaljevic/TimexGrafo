using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using TimexGrafo.Data;
using TimexGrafo.Models;
using TimexGrafo.ViewModels;
using Vereyon.Web;

namespace TimexGrafo.Controllers
{
    [Route("Checkout")]
    public class CheckoutController : Controller
    {
        private readonly ITimexGrafoRepository _repository;

        public CheckoutController(ITimexGrafoRepository repository, IFlashMessage flashMessage)
        {
            _repository = repository;
            FlashMessage = flashMessage;
        }

        public IFlashMessage FlashMessage { get; }

        [HttpPost]
        public IActionResult Index()
        {

            var cartCookie = Request.Cookies["CartCookie"];
            var cart = _repository.GetCartByUserId(cartCookie);
            var cartItem = _repository.GetCartItemByCartId(cart.Id);
            cart.CartItem = cartItem;

            if (cart == null || cart.CartItem.Count <= 0)
            {
                return RedirectToAction("Index", "Korpa");
            }

            foreach (var c in cart.CartItem)
            {
                if(Request.Cookies["cartItem_"+c.Id] != null)
                {
                    c.Quantity = int.Parse(Request.Cookies["cartItem_" + c.Id]);
                }
            }

            var viewModel = new CheckoutViewModel()
            {
                Cart = cart
            };
            return View(viewModel);
        }

        [HttpPost]
        [Route("Submit")]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(CheckoutViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MimeMessage message = new MimeMessage();

                    MailboxAddress from = new MailboxAddress("TimexGrafo Porudžbina",
                    "timexgrafomailer@gmail.com");
                    message.From.Add(from);

                    InternetAddressList list = new InternetAddressList();
                    list.Add(new MailboxAddress(viewModel.FullName, viewModel.Email)); //user email
                    list.Add(new MailboxAddress("Timex Grafo Office", "office@timexgrafo.rs")); //Timexgrafo office email - office@timexgrafo.rs

                    message.To.AddRange(list);

                    message.Subject = "Nova porudžbina";

                    if (viewModel.Note == null)
                    {
                        viewModel.Note = "-------------";
                    }
                    var cartItem = _repository.GetCartItemByCartId(viewModel.CartId);
                    double totalPrice = 0;
                    foreach (var i in cartItem)
                    {
                        totalPrice += i.Quantity * i.Item.FullPrice;
                    }
                    
                    var totalPriceToString = string.Format("{0:0.00}", totalPrice);

                    BodyBuilder bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = "<h1>Timex Grafo - porudžbina </h1><br/>" +
                        "<table style='width:600px'>" +
                        "<tbody>" +
                        "<tr><th>Preduzeće </th><td style='width: 400px'>" + viewModel.Firm + "</td></tr>" +
                        "<tr><th>PIB </th><td style='width: 400px'>" + viewModel.PIB + "</td></tr>" +
                        "<tr><th>Ime i prezime </th><td style='width: 400px'>" + viewModel.FullName + "</td></tr>" +
                        "<tr><th>Adresa </th><td style='width: 400px'>" + viewModel.Address + "</td></tr>" +
                        "<tr><th>Poštanski broj i grad </th><td style='width: 400px'>" + viewModel.PostalNumber + ", " + viewModel.City + "</td></tr>" +
                        "<tr><th>E-pošta </th><td style='width: 400px'>" + viewModel.Email + "</td></tr>" +
                        "<tr><th>Broj telefona </th><td style='width: 400px'>" + viewModel.PhoneNumber + "</td></tr></tbody></table>" +
                        "<h4>Dodatna napomena:</h4><p style='word-break: break-all;width: 300px;'>" + viewModel.Note + "</p><hr/>";

                    bodyBuilder.HtmlBody +=
                       "<h3>Ukupno za naplatu: " + totalPriceToString + " RSD</h3><h4>Naručeni artikli: </h4><hr style='margin-left:0px; width:15%'/>";
                    foreach (var c in cartItem)
                    {
                        bodyBuilder.HtmlBody += "<table><tbody>" +
                            "<tr><th>Šifra artikla: </th><td style='width: 400px'>" + c.Item.ItemCode + "</td></tr>" +
                            "<tr><th>Naziv: </th><td style='width: 400px'>" + c.Item.Title + "</td></tr>" +
                            "<tr><th>Cena bez PDV: </th><td style='width: 400px'>" + c.Item.Price + " RSD</td></tr>" +
                            "<tr><th>Cena sa PDV: </th><td style='width: 400px'>" + c.Item.FullPrice + " RSD</td></tr>" +
                            "<tr><th>Količina: </th><td style='width: 400px'>" + c.Quantity + "</td></tr>" +
                            "<tr><th>Ukupno: </th><td style='width: 400px'>" + c.Item.FullPrice*c.Quantity + " RSD</td></tr>" +
                            "</tbody></table><hr style='margin-left:0px; width:15%'/>";
                    }

                    bodyBuilder.HtmlBody += "<br/><p><strong>Ukoliko postoji problem sa porudžbinom, javite se na email: office@timexgrafo.rs </strong></p>";

                    message.Body = bodyBuilder.ToMessageBody();

                    SmtpClient client = new SmtpClient();
                    client.Connect("Smtp.gmail.com", 465, true);
                    client.Authenticate("timexgrafomailer@gmail.com", "000000");

                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();

                    if (Request.Cookies["CartCookie"] != null)
                    {
                        Response.Cookies.Delete("CartCookie");
                    }

                    foreach (var c in cartItem)
                    {
                        if (Request.Cookies["cartItem_" + c.Id] != null)
                        {
                            Response.Cookies.Delete("cartItem_" + c.Id);
                        }
                    }


                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

                FlashMessage.Confirmation("Uspešno ste poručili artikle. Na email vam je poslata priznanica sa detaljima porudžbine.");
                return RedirectToAction("Shop", "Home");
            }
            else
            {
                var cart = _repository.GetCartByUserId(Request.Cookies["CartCookie"]);
                viewModel.Cart = cart;
                viewModel.Cart.CartItem = _repository.GetCartItemByCartId(cart.Id);
                
                return View("Index", viewModel);
            }
        }
    }
}
