using Vidos.Data.Models;
using Vidos.Services.Mapping;

namespace Vidos.Services.Models.Reviews.ViewModels
{
    public class EditReviewViewModel : IMapTo<Review>
    {
        public string Id { get; set; }

        public int Rating { get; set; }

        public string Body { get; set; }
    }
}
