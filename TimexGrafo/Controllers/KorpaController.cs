using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimexGrafo.Data;
using TimexGrafo.Models;
using TimexGrafo.ViewModels;
using Vereyon.Web;

namespace TimexGrafo.Controllers
{
    [Route("/Korpa")]
    public class KorpaController : Controller
    {
        private readonly ITimexGrafoRepository _repository;
        private readonly IHttpContextAccessor _httpContext;

        public IFlashMessage FlashMessage { get; }

        public KorpaController(ITimexGrafoRepository repository, IFlashMessage flashMessage, IHttpContextAccessor httpContext)
        {
            _repository = repository;
            FlashMessage = flashMessage;
            _httpContext = httpContext;
        } 
        public IActionResult Index()
        {
            if(Request.Cookies["CartCookie"] != null)
            {
                var cartCookie = Request.Cookies["CartCookie"];

                var userId = cartCookie;

                var cart = _repository.GetCartByUserId(userId);
                var cartItem = _repository.GetCartItemByCartId(cart.Id);
                cart.CartItem = cartItem;

                foreach (var c in cart.CartItem)
                {
                    if (Request.Cookies["cartItem_" + c.Id] != null)
                    {
                        c.Quantity = int.Parse(Request.Cookies["cartItem_" + c.Id]);
                    }
                }

                var viewModel = new CartViewModel()
                {
                    Cart = cart
                };

                return View(viewModel);
            }
            else
            {
                return View();
            }
            
        }

        [HttpPost]
        public IActionResult DodajUKorpu(ShopItemDetailsViewModel viewModel)
        {
            var item = _repository.GetShopProductById(viewModel.ItemId);
            //var user = 
            //string userId = 
            //var cart = _repository.GetCartByUserId(userId);

            if (ModelState.IsValid)
            {
                var CartCookie = _httpContext.HttpContext.Request.Cookies["CartCookie"];
                if (CartCookie == null)
                {
                    var cartModel = new Cart();

                    var option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(7);

                    var userId = Guid.NewGuid().ToString();
                    string cartValue = userId.ToString();

                    Response.Cookies.Append("CartCookie", cartValue, option);

                    cartModel.UserId = userId;

                    cartModel.CartItem = new List<CartItem>()
                    {
                        new CartItem()
                        {
                            Item = item,
                            Quantity = viewModel.Quantity,
                            CartId = cartModel.Id
                        }
                    };
                    cartModel.TotalPrice += item.FullPrice * viewModel.Quantity;

                    _repository.AddEntity(cartModel);

                    if (_repository.SaveAll())
                    {
                        //var result = new { Success = "true", message = "Uspešno ste dodali artikal u korpu. " };
                        //return Json(result);
                        FlashMessage.Info("Uspešno ste dodali artikal u korpu. ");
                        return RedirectToAction("Shop", "Home");
                    }
                    else
                    {
                        throw new Exception("Nije uspelo");
                    }
                }
                else
                {
                    item = _repository.GetShopProductById(viewModel.ItemId);
                    viewModel.Item = item;
                    var userId = CartCookie;
                    var cart = _repository.GetCartByUserId(userId);
                    var cartItem = _repository.GetCartItemByCartId(cart.Id);
                    cart.CartItem = cartItem;


                    if (_repository.IsThereSameProductInTheCart(cart.Id, item.Id) != false)
                    {
                        //var result = new { Success = "true", message = "Već ste dodali ovaj artikal u korpu. " };
                        //return Json(result);
                        FlashMessage.Warning("Odabrani artikal se već nalazi u korpi. ");
                        return Redirect("/Shop/" + item.Id);
                    }
                    else
                    {
                        cart.CartItem.Append(
                        new CartItem()
                        {
                            Item = item,
                            Quantity = viewModel.Quantity,
                            CartId = cart.Id
                        }
                    );

                        if(cart.CartItem.Count() <=0)
                        {
                            cart.TotalPrice = item.FullPrice * viewModel.Quantity;
                        }
                        else
                        {
                            cart.TotalPrice += item.FullPrice * viewModel.Quantity;
                        }

                        _repository.AddEntity(new CartItem()
                        {
                            Item = item,
                            Quantity = viewModel.Quantity,
                            CartId = cart.Id
                        });

                        _repository.UpdateCartWithNewCartItem(cart);

                        if (_repository.SaveAll())
                        {
                            //return Json(new { Success = true });
                            FlashMessage.Info("Uspešno ste dodali artikal u korpu. ");
                            return RedirectToAction("Shop", "Home");
                        }
                        else
                        {
                            //var result = new { Success = "true", message = "Uspešno ste dodali artikal u korpu. " };
                            //return Json(result);

                            return RedirectToAction("Shop", "Home");
                        }

                    }
                }
            }
            else
            {
                viewModel.Item = item;
                return View("Index", viewModel);
            }
        }

        [HttpDelete]
        public ActionResult UkloniIzKorpe(string cartItemId, int quantity)
        {
            //var user = _userManager.GetUserAsync(User).Result;
            //string userId = user.Id;
            var CartCookie = _httpContext.HttpContext.Request.Cookies["CartCookie"];
            var userId = CartCookie;
            var cart = _repository.GetCartByUserId(userId);

            CartItem cartItem = _repository.GetCartItemById(int.Parse(cartItemId));

            cart.TotalPrice -= cartItem.Item.FullPrice * quantity;

            _repository.RemoveCartItem(cartItem);

            if (_repository.SaveAll())
            {
                var result = new { Success = "true", message = "Uspešno ste uklonili artikal iz korpe. " };
                return Json(result);
            }
            else
            {
                var result = new { Success = "true", message = "Uspešno ste uklonili artikal iz korpe. " };
                return Json(result);
            }
        }

        [HttpPut]
        public JsonResult UpdateCartItem(string cartItemId, int quantity, int quantityBeforeChange)
        {
            var userId= Request.Cookies["CartCookie"];
            var cart = _repository.GetCartByUserId(userId);
            var cartItem = _repository.GetCartItemByCartId(cart.Id);
            var cItemId = int.Parse(cartItemId);

            foreach (var item in cartItem)
            {
                if (item.Id == int.Parse(cartItemId))
                {
                    var cartItemQuantity = cartItem.SingleOrDefault(id => id.Id == cItemId).Quantity = quantity;
                    cartItem.SingleOrDefault(id => id.Id == cItemId);

                    if (cartItemQuantity == quantityBeforeChange)
                    {
                        continue;
                    }
                    else
                    {
                        if (cartItemQuantity > quantityBeforeChange)
                            cart.TotalPrice += item.Item.FullPrice;

                        else if (cartItemQuantity < quantityBeforeChange)
                            cart.TotalPrice -= item.Item.FullPrice;
                    }
                }
            }

            cart.CartItem = cartItem;

            _repository.UpdateEntity(cart);

            if (_repository.SaveAll())
            {
                var result = new { Success = true, message = cart.Id };
                return Json(result);
            }
            else
            {
                var result = new { Success = false, message = cart.Id };
                return Json(result);
            }

        }       
    }
}
