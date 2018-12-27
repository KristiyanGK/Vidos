using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidos.Services.Mapping;
using Vidos.Services.Models.CartItem.ViewModels;
using Vidos.Web.Common;

namespace Vidos.Services.Models.Order.ViewModels
{
    public class MyOrdersViewModel : IHaveCustomMappings
    {
        [Display(Name = DisplayNames.Address)]
        public string Address { get; set; }

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

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Data.Models.Order, MyOrdersViewModel>()
                .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(src =>
                        src.Line1 + ";" +
                        src.Line2 + ";" +
                        src.Line3));
        }
    }
}
