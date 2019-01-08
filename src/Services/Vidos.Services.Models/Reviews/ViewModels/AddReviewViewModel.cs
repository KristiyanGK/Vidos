using Vidos.Data.Models;
using Vidos.Services.Mapping;

namespace Vidos.Services.Models.Reviews.ViewModels
{
    public class AddReviewViewModel : IMapTo<Review>
    {
        public int Rating { get; set; }

        public string Body { get; set; }
    }
}
