using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TimexGrafo.Models;

namespace TimexGrafo.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Unesite naziv preduzeća. ")]
        [RegularExpression("^[a-zA-ZàáâäãåąčćđęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĐĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$", ErrorMessage = "Nepravilno uneto ime preduzeća. ")]
        public string Firm { get; set; }

        [Required(ErrorMessage = "Unesite PIB. ")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "Nepravilno unet PIB. ")]
        public int? PIB { get; set; }

        [Required(ErrorMessage = "Unesite adresu. ")]
        [RegularExpression(@"^[A-ZŠĐČĆŽ][a-zA-ZŠĐČĆŽšđčćž]{1,}\s([A-Za-zšđčćžŠĐČĆŽ]{1,}\s)*([0-9a-zA-ZšđčćžŠĐČĆŽ/]*\s*)*$", ErrorMessage = "Nepravilno uneta adresa. ")]
        public string Address { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Nepravilno unet poštanski broj. ")]
        [Required(ErrorMessage = "Poštanski broj je obavezan. ")]
        public int? PostalNumber { get; set; }

        [RegularExpression("^[a-zA-ZàáâäãåąčćđęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĐĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$", ErrorMessage = "Nepravilno uneto ime grada. ")]
        [Required(ErrorMessage = "Ime grada je obavezno. ")]
        public string City { get; set; }

        [Required(ErrorMessage = "Unesite ime i prezime. ")]
        [RegularExpression("^[a-zA-ZàáâäãåąčćđęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĐĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$", ErrorMessage = "Nepravilno uneto ime. ")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Unesite adresu e-pošte. ")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Broj telefona je obavezan. ")]
        [Phone(ErrorMessage ="Nepravilno unet broj telefona. ")]
        public string PhoneNumber { get; set; }

        public string Note { get; set; }

        public Cart Cart { get; set; }
        public int CartId { get; set; }
    }
}
