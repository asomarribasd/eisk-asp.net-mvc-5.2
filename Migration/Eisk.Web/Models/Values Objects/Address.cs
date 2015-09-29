using System.ComponentModel.DataAnnotations;
using Eisk.Helpers;

namespace Eisk.Models
{
    public class Address
    {
        [StringLength(60)]
        [Required(ErrorMessage = "Address line required.")]
        [Display(Name = "Address line")]
        public string AddressLine { get; set; }

        [StringLength(15)]
        public string City { get; set; }

        [StringLength(15)]
        [RequiredIf(nameof(Country), "Canada, India, USA", ErrorMessage = "You must specify Region for the selected country.")]
        public string Region { get; set; }

        [StringLength(10)]
        [RegularExpression("\\d{1,10}",
            ErrorMessage = "Not a valid postal code. Please consider upto 10 digit for valid phone format.")]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country required.")]
        [StringLength(15)]
        [UIHint("CountryDropDown")]
        public string Country { get; set; }
    }
}