using AutoMapper;
using Vidos.Services.Mapping;

namespace Vidos.Services.Models.CartItem.ViewModels
{
    public class CartItemIndexViewModel : IHaveCustomMappings
    {
        public int Quantity { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Data.Models.CartItem, CartItemIndexViewModel>()
                .ForMember(dest => dest.ProductId,
                    opt => opt.MapFrom(src => src.AirConditioner.Id))
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.AirConditioner.Name))
                .ForMember(dest => dest.ProductPrice,
                    opt => opt.MapFrom(src => src.AirConditioner.Price));
        }
    }
}
