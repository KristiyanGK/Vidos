﻿using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using Vidos.Services.Mapping;
using Vidos.Web.Common.Constants;

namespace Vidos.Services.Models.Order.ViewModels
{
    public class AllOrdersViewModel : IHaveCustomMappings
    {
        public string Id { get; set; }

        [Display(Name = DisplayNames.ClientName)]
        public string ClientName { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = DisplayNames.PurchaseDate)]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = DisplayNames.Line1)]
        public string Line1 { get; set; }

        [Required]
        [Display(Name = DisplayNames.City)]
        public string City { get; set; }

        [Required]
        [Display(Name = DisplayNames.State)]
        public string State { get; set; }

        [Required]
        [Display(Name = DisplayNames.Country)]
        public string Country { get; set; }

        [Display(Name = DisplayNames.IsShipped)]
        public bool IsShipped { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Data.Models.Order, AllOrdersViewModel>()
                .ForMember(dest => dest.ClientName,
                    opt => opt.MapFrom(src => src.Client.FirstName + " " + src.Client.LastName));
        }
    }
}
