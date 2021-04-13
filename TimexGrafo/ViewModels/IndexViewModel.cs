using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimexGrafo.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Models.ShopGroup> ShopGroups { get; set; }
        public IEnumerable<Models.Group> Groups { get; set; }
    }
}
