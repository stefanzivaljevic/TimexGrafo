using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimexGrafo.Models
{
    public class ShopSubGroup
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<ShopItem> Items { get; set; }
    }
}
