using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.SqlServer.Server;
using Vidos.Data.Models;
using Vidos.Services.Mapping;
using Vidos.Web.Common.Attributes;
using Vidos.Web.Common.Constants;

namespace Vidos.Services.Models.Reviews.ViewModels
{
    public class AddReviewViewModel : IMapTo<Review>
    {
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
