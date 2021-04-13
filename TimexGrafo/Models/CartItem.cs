using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimexGrafo.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public ShopItem Item { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Size { get; set; }
        public Cart Cart { get; set; }
        public int CartId { get; set; }

    }
}
