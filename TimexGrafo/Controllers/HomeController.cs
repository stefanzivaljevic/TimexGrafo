using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleMvcSitemap;
using TimexGrafo.Data;
using TimexGrafo.Models;
using TimexGrafo.ViewModels;

namespace TimexGrafo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel()
            {
                ShopGroups = _context.ShopGroups.Include(c => c.SubGroups).ThenInclude(e => e.Items),
                Groups = _context.Groups.Include(c => c.SubGroups).ThenInclude(e => e.Items)
            };
            ViewBag.ActiveMenu = "nav-link-pocetna";
            return View(viewModel);
            //var viewModel = new VeleprodajaViewModel()
            //{
            //    Groups = _context.Groups.Include(c => c.SubGroups).ThenInclude(e => e.Items)
            //};
            //if (Request.Cookies["ConsentCookie"] != null)
            //{

            //    var viewModel = new IndexViewModel()
            //    {
            //        ShopGroups = _context.ShopGroups.Include(c => c.SubGroups).ThenInclude(e => e.Items),
            //        Groups = _context.Groups.Include(c => c.SubGroups).ThenInclude(e => e.Items)
            //    };
            //    ViewBag.ActiveMenu = "nav-link-pocetna";
            //    return View(viewModel);
            //}
            //else
            //{
            //    //var option = new CookieOptions();
            //    //option.Expires = DateTime.Now.AddDays(365);

            //    //Response.Cookies.Append("ConsentCookie","Consented", option);
            //    return View();
            //}



        }

        [Route("/Shop")]
        public IActionResult Shop()
        {
            var viewModel = new IndexViewModel()
            {
                ShopGroups = _context.ShopGroups.Include(c => c.SubGroups).ThenInclude(e => e.Items)
            };

            ViewBag.ActiveMenu = "nav-link-shop";

            return View(viewModel);
        }

        [Route("/Veleprodaja")]
        public IActionResult Veleprodaja()
        {
            var viewModel = new VeleprodajaViewModel()
            {
                Groups = _context.Groups.Include(c=>c.SubGroups).ThenInclude(e=>e.Items)
            };
            
            ViewBag.ActiveMenu = "nav-link-veleprodaja";
            
            return View(viewModel);
        }

        [Route("/Proizvodnja")]
        public IActionResult Proizvodnja()
        {
            ViewBag.ActiveMenu = "nav-link-proizvodnja";
            return View();
        }

        [Route("/Onama")]
        public IActionResult Onama()
        {
            ViewBag.ActiveMenu = "nav-link-onama";
            return View();
        }

        //[Route("/Kontakt")]
        //public IActionResult Kontakt()
        //{
        //    ViewBag.ActiveMenu = "nav-link-kontakt";
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
