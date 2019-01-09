using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Web.Common.Exceptions;

namespace Vidos.Services.DataServices
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review> _reviewRepository;
        private readonly IRepository<AirConditioner> _productRepository;

        public ReviewService(IRepository<Review> reviewRepository,
            IRepository<AirConditioner> productRepository)
        {
            this._reviewRepository = reviewRepository;
            this._productRepository = productRepository;
        }

        public async Task<Review> AddReviewAsync(Review review, string authorId, string productId)
        {
            review.ClientId = authorId;

            review.ProductId = productId;

            var result = await this._reviewRepository.AddAsync(review);

            await this._reviewRepository.SaveChangesAsync();

            return result;
        }

        public async Task RemoveReviewByIdAsync(string id)
        {
            var review = this._reviewRepository.FindById(id);

            if (review != null)
            {
                this._reviewRepository.Delete(review);

                await this._reviewRepository.SaveChangesAsync();
            }
        }

        public async Task<Review> UpdateReviewAsync(Review review)
        {
            var oldReview = this._reviewRepository.FindById(review.Id);

            if (oldReview != null)
            {
                oldReview.Body = review.Body;
                oldReview.Rating = review.Rating;

                await this._reviewRepository.SaveChangesAsync();
            }

            return oldReview;
        }

        public bool HasUserReviewedCurrentProduct(string userId, string productId)
        {
            var product = this._productRepository.All()?
                .Include(p => p.Reviews)
                .ThenInclude(pr => pr.Client)
                .FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            return product.Reviews.Any(r => r.ClientId == userId);
        }

        public IQueryable<Review> GetUserReviewsById(string userId)
        {
            var reviews =
                this._reviewRepository
                    .All()
                    .Include(r => r.Client)
                    .Where(r => r.ClientId == userId);

            return reviews;
        }

        public Review GetReviewById(string reviewId)
        {
            return this._reviewRepository.FindById(reviewId);
        }

        public bool IsReviewOwnedByUser(string userId, string reviewId)
        {
            var reviews = this._reviewRepository.FindById(reviewId);

            if (reviews == null)
            {
                return false;
            }

            return reviews.ClientId == userId;
        }
    }
}
