using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Moq;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.DataServices;
using Vidos.Web.Common.Exceptions;
using Xunit;

namespace Vidos.Services.Tests.ServicesTests
{
    public class ReviewServiceTests
    {
        private readonly Mock<IRepository<Review>> _reviewRepoMock;
        private readonly Mock<IRepository<AirConditioner>> _productRepoMock;

        private List<AirConditioner> sampleProducts = new List<AirConditioner>
        {
            new AirConditioner
            {
                Id = "prod1",
                Reviews = new List<Review>
                {
                    new Review
                    {
                        Client = new VidosUser
                        {
                            Id = "user1"
                        }
                    }
                }
            },new AirConditioner
            {
                Id = "prod2",
                Reviews = new List<Review>
                {
                    new Review
                    {
                        Client = new VidosUser
                        {
                            Id = "user2"
                        }
                    }
                }
            },new AirConditioner
            {
                Id = "prod3",
                Reviews = new List<Review>
                {
                    new Review
                    {
                        Client = new VidosUser
                        {
                            Id = "user3"
                        }
                    }
                }
            }
        };

        private List<Review> sampleReviews = new List<Review>
        {
            new Review
            {
                Id = "rev1",
                Client = new VidosUser
                {
                    Id = "user1"
                }
            },
            new Review
            {
                Id = "rev2",
                Client = new VidosUser
                {
                    Id = "user2"
                }
            },
            new Review
            {
                Id = "rev3",
                Client = new VidosUser
                {
                    Id = "user3"
                }
            },
        };

        public ReviewServiceTests()
        {
            _reviewRepoMock = new Mock<IRepository<Review>>();
            _productRepoMock = new Mock<IRepository<AirConditioner>>();
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("2", "3")]
        [InlineData("12", "5")]
        public async Task AddReviewAsyncShouldReturnCorrectProduct(
            string authorId,
            string productId)
        {
            Review review = new Review
            {
                ClientId = authorId,
                ProductId = productId
            };

            this._reviewRepoMock
                .Setup(r => r.AddAsync(It.IsAny<Review>()))
                .Returns((Review rev) => Task.FromResult(rev));

            this._reviewRepoMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.FromResult(1));

            var service = new ReviewService(this._reviewRepoMock.Object, null);

            var result = await service.AddReviewAsync(review, authorId, productId);

            Assert.Equal(review, result);
        }

        [Theory]
        [InlineData("new", 1)]
        [InlineData("new2", 2)]
        [InlineData("new3", 3)]
        public async Task UpdateReviewAsyncShouldUpdateCorrectly(string body, int rating)
        {
            Review newData = new Review
            {
                Id = "1",
                Body = body,
                Rating = rating
            };

            this._reviewRepoMock
                .Setup(r => r.FindById(It.IsAny<string>()))
                .Returns((string pId) => new Review
                {
                    Id = pId,
                    Body = "old",
                    Rating = 5
                });

            this._reviewRepoMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.FromResult(1));

            var service = new ReviewService(this._reviewRepoMock.Object, null);

            var result = await service.UpdateReviewAsync(newData);

            bool isEqual = newData.Rating == result.Rating && newData.Body == result.Body;

            Assert.True(isEqual);
        }

        [Theory]
        [InlineData("user1", "prod1")]
        [InlineData("user2", "prod2")]
        [InlineData("user3", "prod3")]
        [InlineData("user1", "prod3")]
        [InlineData("not a user", "prod3")]
        public void HasUserReviewedCurrentProductShouldReturnCorrectBool(
            string userId, string productId)
        {
            this._productRepoMock
                .Setup(r => r.All())
                .Returns(sampleProducts.AsQueryable());

            var service = new ReviewService(
                null,
                this._productRepoMock.Object);

            var result = service.HasUserReviewedCurrentProduct(userId, productId);

            var expectedResult =
                sampleProducts
                    .FirstOrDefault(p => p.Id == productId)
                    .Reviews
                    .Any(p => p.ClientId == userId);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void
            HasUserReviewedCurrentProductShouldThrowProductNotFoundExceptionWhenProductIsNull()
        {
            this._productRepoMock
                .Setup(r => r.All())
                .Returns(default(IQueryable<AirConditioner>));

            var service = new ReviewService(
                null,
                this._productRepoMock.Object);

            Assert.Throws<ProductNotFoundException>(() => 
                service.HasUserReviewedCurrentProduct(
                string.Empty,
                string.Empty));
        }

        [Theory]
        [InlineData("user1")]
        [InlineData("user2")]
        [InlineData("user3")]
        public void GetUserReviewsByIdShouldReturnCorrectReviews(string id)
        {
            this._reviewRepoMock
                .Setup(r => r.All())
                .Returns(sampleReviews.AsQueryable());

            var service = new ReviewService(this._reviewRepoMock.Object, null);

            var result = service.GetUserReviewsById(id).AsEnumerable();

            var expectedResult = sampleReviews.Where(r => r.ClientId == id);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GetUserReviewsByIdShouldReturnEmptyCollectionIfNoneAreFound()
        {
            this._reviewRepoMock
                .Setup(r => r.All())
                .Returns(new List<Review>().AsQueryable());

            var service = new ReviewService(this._reviewRepoMock.Object, null);

            var result = service.GetUserReviewsById(string.Empty).AsEnumerable();

            Assert.Empty(result);
        }

        [Theory]
        [InlineData("rev1")]
        [InlineData("rev2")]
        [InlineData("rev3")]
        public void GetReviewByIdShouldReturnCorrectReview(string reviewId)
        {
            this._reviewRepoMock
                .Setup(r => r.FindById(It.IsAny<string>()))
                .Returns((string id) => new Review
                {
                    Id = id
                });

            var service = new ReviewService(this._reviewRepoMock.Object, null);

            var result = service.GetReviewById(reviewId);

            var expectedResult = sampleReviews.FirstOrDefault(r => r.Id == reviewId);

            Assert.Equal(result.Id, expectedResult.Id);
        }

        [Fact]
        public void GetReviewByIdShouldReturnNullIfNotFound()
        {
            this._reviewRepoMock
                .Setup(r => r.FindById(It.IsAny<string>()))
                .Returns(default(Review));

            var service = new ReviewService(this._reviewRepoMock.Object, null);

            var result = service.GetReviewById(string.Empty);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("user1", "rev1")]
        [InlineData("user2", "rev2")]
        [InlineData("user3", "rev3")]
        [InlineData("notAUser", "rev1")]
        [InlineData("user2", "rev6")]
        [InlineData("", "")]
        public void IsReviewOwnedByUserShouldReturnCorrectBool(
            string userId, string reviewId)
        {
            this._reviewRepoMock
                .Setup(r => r.FindById(It.IsAny<string>()))
                .Returns((string id) => sampleReviews.FirstOrDefault(rev => rev.Id == id));

            var service = new ReviewService(this._reviewRepoMock.Object, null);

            var result = service.IsReviewOwnedByUser(userId, reviewId);

            var expectedResult = sampleReviews.FirstOrDefault(rev => rev.Id == reviewId)?.ClientId == userId;

            Assert.Equal(expectedResult, result);
        }
    }
}
