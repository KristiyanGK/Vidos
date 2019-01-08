using System.Linq;
using System.Threading.Tasks;
using Vidos.Data.Models;

namespace Vidos.Services.DataServices.Contracts
{
    public interface IReviewService
    {
        Task<Review> AddReviewAsync(Review review, string authorId, string productId);

        Task RemoveReviewByIdAsync(string id);

        Task<Review> UpdateReviewAsync(Review review);

        bool HasUserReviewedCurrentProduct(string userId, string productId);

        IQueryable<Review> GetUserReviewsById(string userId);

        Review GetReviewById(string reviewId);
    }
}
