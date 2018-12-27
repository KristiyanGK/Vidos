using AutoMapper;
using Vidos.Services.Mapping;

namespace Vidos.Services.Models.CartItem.ViewModels
{
    public class CartItemDetailsViewModel : IHaveCustomMappings
    {
        public int Quantity { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Data.Models.CartItem, CartItemDetailsViewModel>()
                .ForMember(dest => dest.ProductId,
                    opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name));
        }
    }
}
