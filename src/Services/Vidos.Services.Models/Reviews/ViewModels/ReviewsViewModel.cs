using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Vidos.Data.Models;
using Vidos.Services.Mapping;
using Vidos.Web.Common.Constants;

namespace Vidos.Services.Models.Reviews.ViewModels
{
    public class ReviewsViewModel : IHaveCustomMappings
    {
        [Display(Name = DisplayNames.Rating)]
        public int Rating { get; set; }

        [Display(Name = DisplayNames.ReviewBody)]
        public string Body { get; set; }

        [Display(Name = DisplayNames.Author)]
        public string Author { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Review, ReviewsViewModel>()
                .ForMember(dest => dest.Author,
                    opt => opt.MapFrom(src => src.Client.FirstName));
        }
    }
}
