using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimexGrafo.Models;

namespace TimexGrafo.ViewModels
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            Cart = new Cart();
            Item = new ShopItem();
        }

        public Cart Cart { get; set; }
        public ShopItem Item { get; set; }
    }
}
