using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Vidos.Services.Mapping;

namespace Vidos.Services.Models.Order.ViewModels
{
    public class AllOrdersViewModel : IHaveCustomMappings
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

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Data.Models.Order, AllOrdersViewModel>()
                .ForMember(dest => dest.ClientName,
                    opt => opt.MapFrom(src => src.Client.FirstName + " " + src.Client.LastName));
        }
    }
}
