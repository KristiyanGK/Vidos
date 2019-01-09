using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidos.Services.Mapping;
using Vidos.Services.Models.CartItem.ViewModels;
using Vidos.Web.Common.Constants;

namespace Vidos.Services.Models.Order.ViewModels
{
    public class OrderDetailsClientViewModel : IMapFrom<Data.Models.Order>
    {
        [Display(Name = DisplayNames.Line1)]
        public string Line1 { get; set; }

        [Display(Name = DisplayNames.Line2)]
        public string Line2 { get; set; }

        [Display(Name = DisplayNames.Line3)]
        public string Line3 { get; set; }

        [Display(Name = DisplayNames.City)]
        public string City { get; set; }

        [Display(Name = DisplayNames.State)]
        public string State { get; set; }

        [Display(Name = DisplayNames.Zip)]
        public string Zip { get; set; }

        [Display(Name = DisplayNames.Country)]
        public string Country { get; set; }

        [Display(Name = DisplayNames.IsShipped)]
        public bool IsShipped { get; set; }

        [Display(Name = DisplayNames.PurchaseDate)]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = DisplayNames.Items)]
        public ICollection<CartItemDetailsViewModel> Items { get; set; }
    }
}
