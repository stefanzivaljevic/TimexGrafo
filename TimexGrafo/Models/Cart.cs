using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimexGrafo.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IList<CartItem> CartItem { get; set; }
        public double TotalPrice { get; set; }
    }
}
