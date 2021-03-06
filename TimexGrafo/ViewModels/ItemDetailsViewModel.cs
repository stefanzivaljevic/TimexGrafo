using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TimexGrafo.Models;

namespace TimexGrafo.ViewModels
{
    public class ItemDetailsViewModel
    {
        public Item Item { get; set; }

        public int ItemId { get; set; }

        [Range(1, 9999, ErrorMessage = "Možete poručiti najmanje 1, a najviše 9999 artikala. ")]
        public int Quantity { get; set; }
    }
}
