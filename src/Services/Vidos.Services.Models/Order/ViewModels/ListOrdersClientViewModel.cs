using System;
using System.ComponentModel.DataAnnotations;
using Vidos.Services.Mapping;
using Vidos.Web.Common.Constants;

namespace Vidos.Services.Models.Order.ViewModels
{
    public class ListOrdersClientViewModel 
        : IMapFrom<Data.Models.Order>
    {
        public string Id { get; set; }

        [Display(Name = DisplayNames.Line1)]
        public string Line1 { get; set; }

        [Display(Name = DisplayNames.City)]
        public string City { get; set; }

        [Display(Name = DisplayNames.State)]
        public string State { get; set; }

        public string Zip { get; set; }

        [Display(Name = DisplayNames.Country)]
        public string Country { get; set; }

        [Display(Name = DisplayNames.IsShipped)]
        public bool IsShipped { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = DisplayNames.PurchaseDate)]
        public DateTime PurchaseDate { get; set; }
    }
}
