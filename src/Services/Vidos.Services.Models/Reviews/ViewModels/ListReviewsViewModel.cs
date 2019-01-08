using AutoMapper;
using Vidos.Data.Models;
using Vidos.Services.Mapping;

namespace Vidos.Services.Models.Reviews.ViewModels
{
    public class ListReviewsViewModel : IHaveCustomMappings
    {
        public string Id { get; set; }

        public int Rating { get; set; }

        public string Body { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Review, ListReviewsViewModel>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name));
        }
    }
}
