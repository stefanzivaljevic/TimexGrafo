using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimexGrafo.Data;
using TimexGrafo.ViewModels;

namespace TimexGrafo.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Shop/{groupId:int}/{subGroupId:int}/{id:int}")]
        public IActionResult Details(int groupId, int subGroupId, int id)
        {
            var viewModel = new ShopItemDetailsViewModel()
            {
                Item = _context.ShopItems.SingleOrDefault(i => i.Id == id),
                SubGroup = _context.ShopSubGroups.SingleOrDefault(s=>s.Id == subGroupId),
                Group = _context.ShopGroups.SingleOrDefault(g => g.Id == groupId )
            };
            if (viewModel.Item == null)
                return RedirectToAction("Error", "Home");
            return View(viewModel);
        }
    }
}
