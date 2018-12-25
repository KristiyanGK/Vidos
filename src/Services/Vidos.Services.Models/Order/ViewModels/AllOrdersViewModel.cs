using System;
using System.ComponentModel.DataAnnotations;
using Vidos.Services.Mapping;

namespace Vidos.Services.Models.Order.ViewModels
{
    public class AllOrdersViewModel : IMapFrom<Data.Models.Order>
    {
        public string ClientName { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        public string Zip { get; set; }

        [Required]
        public string Country { get; set; }

        public bool IsShipped { get; set; }
    }
}
