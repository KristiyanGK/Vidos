using AutoMapper;
using Vidos.Data.Models;
using Vidos.Services.Mapping;

namespace Vidos.Services.Models.Product.ViewModels
{
    public class ProductDetailsViewModel : IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string Description { get; set; }

        public string BrandName { get; set; }

        public decimal Price { get; set; }

        public string Origin { get; set; }

        public double Cooling { get; set; } // kW

        public double Heating { get; set; } // kW

        public double HeatingConsumption { get; set; } // kW

        public double CoolingConsumption { get; set; } // kW

        public string ReturnUrl { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<AirConditioner, ProductDetailsViewModel>()
                .ForMember(dest => dest.BrandName,
                    opt => opt.MapFrom(src => src.Brand.Name));
        }
    }
}
