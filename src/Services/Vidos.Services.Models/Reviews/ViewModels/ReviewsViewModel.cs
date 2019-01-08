using AutoMapper;
using Vidos.Data.Models;
using Vidos.Services.Mapping;

namespace Vidos.Services.Models.Reviews.ViewModels
{
    public class ReviewsViewModel : IHaveCustomMappings
    {
        public int Rating { get; set; }

        public string Body { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Review, ReviewsViewModel>()
                .ForMember(dest => dest.Author,
                    opt => opt.MapFrom(src => src.Client.FirstName));
        }
    }
}
