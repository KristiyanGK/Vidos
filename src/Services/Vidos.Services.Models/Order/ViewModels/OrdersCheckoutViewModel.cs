using System.ComponentModel.DataAnnotations;
using Vidos.Services.Mapping;

namespace Vidos.Services.Models.Order.ViewModels
{
    public class OrderCheckoutViewModel : IMapTo<Data.Models.Order>
    {
        [Required(ErrorMessage = "Моля попълнете адреса")]
        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        [Required(ErrorMessage = "Моля попълнете град")]
        public string City { get; set; }

        [Required(ErrorMessage = "Моля попълнете област")]
        public string State { get; set; }

        public string Zip { get; set; }

        [Required(ErrorMessage = "Моля попълнете държава")]
        public string Country { get; set; }
    }
}
