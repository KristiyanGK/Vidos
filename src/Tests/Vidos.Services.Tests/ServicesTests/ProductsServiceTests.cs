using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.DataServices;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.DataServices.Enums;
using Vidos.Services.Models.Product.ViewModels;
using Vidos.Web.Common.Constants;
using Vidos.Web.Common.Exceptions;
using Xunit;

namespace Vidos.Services.Tests.ServicesTests
{
    public class ProductsServiceTests
    {
        private Mock<IRepository<AirConditioner>> repo;
        private Mock<IBrandService> brandServiceMock;

        private List<AirConditioner> sampleProducts = new List<AirConditioner>
        {
            new AirConditioner
            {
                Id = "1",
                Brand = new Brand
                {
                    Name = Constants.Daikin
                },
                TimesBought = 1
            },
            new AirConditioner
            {
                Id = "2",
                Brand = new Brand
                {
                    Name = Constants.Mitsubishi
                },
                TimesBought = 2
            },
            new AirConditioner
            {
                Id = "3",
                Brand = new Brand
                {
                    Name = Constants.Fujitsu
                },
                TimesBought = 3
            },
        };

        public ProductsServiceTests()
        {
            repo = new Mock<IRepository<AirConditioner>>();
            brandServiceMock = new Mock<IBrandService>();
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

        [Fact]
        public async Task AddAsyncShouldReturnCorrectProduct()
        {
            ProductsCreateViewModel productModel = new ProductsCreateViewModel
            {
                BrandName = "brand",
                Name = "Product"
            };

            repo
                .Setup(r => r.AddAsync(It.IsAny<AirConditioner>()))
                .Returns((AirConditioner p) => Task.FromResult(new AirConditioner
                {
                    BrandId = p.BrandId,
                    Name = p.Name
                }));

            brandServiceMock
                .Setup(r => r.GetBrandByNameAsync(It.IsAny<string>()))
                .Returns((string b) => Task.FromResult(new Brand
                {
                    Name = b
                }));

            Mapper.Initialize(configuration =>
            {
                configuration.CreateMap<ProductsCreateViewModel, AirConditioner>();
            });

            var service = new ProductsService(repo.Object, brandServiceMock.Object);

            var result = await service.AddAsync(productModel);

            var expectedResult = new AirConditioner
            {
                Name = "Product"
            };

            Assert.Equal(expectedResult.Name, result.Name);
        }

        [Fact]
        public async Task AddAsyncShouldReturnNullIfBrandIsNotFound()
        {
            ProductsCreateViewModel productModel = null;

            brandServiceMock
                .Setup(r => r.GetBrandByNameAsync(It.IsAny<string>()))
                .Returns((string b) => Task.FromResult(default(Brand)));

            var service = new ProductsService(null, brandServiceMock.Object);

            var result = await service.AddAsync(productModel);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("1", 3)]
        [InlineData("2", 5)]
        [InlineData("3", 12)]
        public async Task IncreaseTimesBoughtAsyncShouldIncreaseCorrectly(
            string productId, int count)
        {
            var expectedResult = sampleProducts.FirstOrDefault(p => p.Id == productId);

            repo
                .Setup(r => r.FindById(It.IsAny<string>()))
                .Returns((string id) => new AirConditioner
                {
                    Id = id,
                    TimesBought = expectedResult.TimesBought
                });

            repo
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.FromResult(1));

            var service = new ProductsService(repo.Object, null);

            var result = await service.IncreaseTimesBoughtAsync(productId, count);

            expectedResult.TimesBought += count;

            Assert.Equal(expectedResult.TimesBought, result.TimesBought);
        }

        [Fact]
        public async Task IncreaseTimesBoughtAsyncShouldReturnNullIfCountLessThanZero()
        {
            var service = new ProductsService(null, null);

            var result = await service.IncreaseTimesBoughtAsync(string.Empty, -1);

            Assert.Null(result);
        }

        [Fact]
        public async Task 
            IncreaseTimesBoughtAsyncShouldThrowProductNotFoundExceptionWhenProductNull()
        {
            repo
                .Setup(r => r.FindById(It.IsAny<string>()))
                .Returns((string id) => default(AirConditioner));

            var service = new ProductsService(repo.Object, null);

            await Assert.ThrowsAsync<ProductNotFoundException>(
                () => 
                    service.IncreaseTimesBoughtAsync(string.Empty, default(int)));
        }
    }
}
