using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimexGrafo.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<SubGroup> SubGroups { get; set; }

    }
}
