using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.DataServices;
using Vidos.Services.DataServices.Enums;
using Vidos.Web.Common.Constants;
using Vidos.Web.Common.Exceptions;
using Xunit;

namespace Vidos.Services.Tests.ServicesTests
{
    public class ProductsServiceTests
    {
        private Mock<IRepository<AirConditioner>> repo;

        private List<AirConditioner> sampleProducts = new List<AirConditioner>
        {
            new AirConditioner()
            {
                Id = "1",
                Brand = new Brand()
                {
                    Name = Constants.Daikin
                },
                TimesBought = 1
            },
            new AirConditioner()
            {
                Id = "2",
                Brand = new Brand()
                {
                    Name = Constants.Mitsubishi
                },
                TimesBought = 2
            },
            new AirConditioner()
            {
                Id = "3",
                Brand = new Brand()
                {
                    Name = Constants.Fujitsu
                },
                TimesBought = 3
            },
        };

        public ProductsServiceTests()
        {
            repo = new Mock<IRepository<AirConditioner>>();
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        public void GetProductByIdShouldReturnCorrectProduct(string id)
        {
            repo.Setup(r => r.All())
                .Returns(sampleProducts.AsQueryable());

            var service = new ProductsService(repo.Object, null);

            var product = service.GetProductById(id);

            Assert.Equal(id, product.Id);
        }

        [Fact]
        public void GetProductByIdShouldThrowExceptionWhenProductIsNotFound()
        {
            string id = "testId";

            repo.Setup(r => r.All())
                .Returns(new List<AirConditioner>().AsQueryable());

            var service = new ProductsService(repo.Object, null);

            Assert.Throws<ProductNotFoundException>(() => service.GetProductById(id));
        }

        [Fact]
        public async Task GetAllAsyncShouldReturnCorrectProducts()
        {
            repo.Setup(r => r.All())
                .Returns(sampleProducts.AsQueryable());

            var service = new ProductsService(repo.Object, null);

            var result = await service.GetAllAsync(string.Empty, string.Empty);

            var list = result.ToList();

            Assert.Equal(sampleProducts, result);
        }

        [Theory]
        [InlineData(Constants.Mitsubishi, "1")]
        [InlineData(Constants.Fujitsu, "0")]
        [InlineData(Constants.Daikin, "2")]
        [InlineData(Constants.Daikin, "")]
        [InlineData("", "1")]
        public async Task GetAllAsyncWithParamsShouldReturnCorrectProducts
            (string brandName, string priceSort)
        {
            repo.Setup(r => r.All())
                .Returns(sampleProducts.AsQueryable());

            var service = new ProductsService(repo.Object, null);

            var result = await service.GetAllAsync(brandName, priceSort);

            IEnumerable<AirConditioner> expectedResult = sampleProducts;

            if (!string.IsNullOrEmpty(brandName) && !string.IsNullOrWhiteSpace(brandName))
            {
                expectedResult = expectedResult.Where(p => p.Brand.Name == brandName);
            }

            bool hasParsed = Enum.TryParse(priceSort, out ProductPriceSortEnums sort);

            if (hasParsed)
            {
                switch (sort)
                {
                    case ProductPriceSortEnums.Ascending:
                        expectedResult = expectedResult.OrderBy(p => p.Price);
                        break;
                    case ProductPriceSortEnums.Descending:
                        expectedResult = expectedResult.OrderByDescending(p => p.Price);
                        break;
                }
            }
            
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task MostBoughtProductsShouldReturnCorrectProducts(int count)
        {
            repo.Setup(r => r.All())
                .Returns(sampleProducts.AsQueryable());

            var service = new ProductsService(repo.Object, null);

            var result = await service.MostBoughtProductsAsync(count);

            var expectedResult = sampleProducts.OrderBy(p => p.TimesBought).Take(count);

            Assert.Equal(expectedResult, result);
        }
    }
}
