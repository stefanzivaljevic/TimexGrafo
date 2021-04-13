﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimexGrafo.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int Image { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public double FullPrice { get; set; }
        public SubGroup SubGroup { get; set; }
        public int SubGroupId { get; set; }

        public string ItemCode { get; set; }


    }
}
