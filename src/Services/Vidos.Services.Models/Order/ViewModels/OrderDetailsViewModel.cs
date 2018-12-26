using AutoMapper;
using System;
using System.Collections.Generic;
using Vidos.Services.Mapping;
using Vidos.Services.Models.CartItem.ViewModels;

namespace Vidos.Services.Models.Order.ViewModels
{
    public class OrderDetailsViewModel : IHaveCustomMappings
    {
        public string ClientName { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Country { get; set; }

        public bool IsShipped { get; set; }

        public DateTime PurchaseDate { get; set; }

        public ICollection<CartItemDetailsViewModel> Items { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration
                .CreateMap<Data.Models.Order, OrderDetailsViewModel>()
                .ForMember(dest => dest.ClientName,
                    opt => opt.MapFrom(src => src.Client.FirstName + " " + src.Client.LastName));
        }
    }
}
