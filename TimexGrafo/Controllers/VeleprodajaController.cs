using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimexGrafo.Data;
using TimexGrafo.ViewModels;

namespace TimexGrafo.Controllers
{
    public class VeleprodajaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VeleprodajaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Veleprodaja/{id:int}")]
        public IActionResult Details(int id)
        {
            var viewModel = new ItemDetailsViewModel()
            {
                Item = _context.Items.SingleOrDefault(i=>i.Id == id)
        };
            if(viewModel.Item == null)
                return RedirectToAction("Error","Home");
            return View(viewModel);
        }
    }
}