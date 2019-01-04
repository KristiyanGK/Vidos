using System.ComponentModel.DataAnnotations;
using Vidos.Data.Models;
using Vidos.Services.Mapping;
using Vidos.Web.Common.Constants;

namespace Vidos.Services.Models.Order.ViewModels
{
    public class OrderCheckoutViewModel : IMapTo<Data.Models.Order>
    {
        [Required(ErrorMessage = ErrorMessages.FirstNameRequired)]
        [Display(Name = DisplayNames.FirstName)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = ErrorMessages.LastNameRequired)]
        [Display(Name = DisplayNames.LastName)]
        public string LastName { get; set; }

        [Required(ErrorMessage = ErrorMessages.EmailRequired)]
        [Display(Name = DisplayNames.Email)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessages.AddressRequired)]
        [Display(Name = DisplayNames.Line1)]
        public string Line1 { get; set; }

        [Display(Name = DisplayNames.Line2)]
        public string Line2 { get; set; }

        [Display(Name = DisplayNames.Line3)]
        public string Line3 { get; set; }

        [Required(ErrorMessage = ErrorMessages.CityRequired)]
        [Display(Name = DisplayNames.City)]
        public string City { get; set; }

        [Required(ErrorMessage = ErrorMessages.StateRequired)]
        [Display(Name = DisplayNames.State)]
        public string State { get; set; }

        [Display(Name = DisplayNames.Zip)]
        public string Zip { get; set; }

        [Required(ErrorMessage = ErrorMessages.CountryRequired)]
        [Display(Name = DisplayNames.Country)]
        public string Country { get; set; }

        [Required]
        [Display(Name = DisplayNames.PaymentMethod)]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
