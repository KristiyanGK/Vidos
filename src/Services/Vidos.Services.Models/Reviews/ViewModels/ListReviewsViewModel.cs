using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Vidos.Data.Models;
using Vidos.Services.Mapping;
using Vidos.Web.Common.Constants;

namespace Vidos.Services.Models.Reviews.ViewModels
{
    public class ListReviewsViewModel : IHaveCustomMappings
    {
        [Required]
        public string Id { get; set; }

        [Display(Name = DisplayNames.Rating)]
        public int Rating { get; set; }

        [Display(Name = DisplayNames.ReviewBody)]
        public string Body { get; set; }

        [Required]
        public string ProductId { get; set; }

        [Display(Name = DisplayNames.ProductName)]
        public string ProductName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Review, ListReviewsViewModel>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name));
        }
    }
}
