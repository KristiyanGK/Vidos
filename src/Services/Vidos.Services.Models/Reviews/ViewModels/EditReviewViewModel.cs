using System.ComponentModel.DataAnnotations;
using Vidos.Data.Models;
using Vidos.Services.Mapping;
using Vidos.Web.Common.Attributes;
using Vidos.Web.Common.Constants;

namespace Vidos.Services.Models.Reviews.ViewModels
{
    public class EditReviewViewModel : IMapTo<Review>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [ValidRating]
        [Display(Name = DisplayNames.Rating)]
        public int Rating { get; set; }

        [Required]
        [StringLength(Constants.ReviewBodyMaxSize,
            ErrorMessage = ErrorMessages.InvalidReviewBodySize,
            MinimumLength = Constants.ReviewBodyMinSize)]
        [Display(Name = DisplayNames.ReviewBody)]
        public string Body { get; set; }
    }
}
