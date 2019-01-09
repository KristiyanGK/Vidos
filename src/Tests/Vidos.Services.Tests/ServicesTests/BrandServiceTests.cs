using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.DataServices;
using Xunit;

namespace Vidos.Services.Tests.ServicesTests
{
    public class BrandServiceTests
    {
        private Mock<IRepository<Brand>> brandRepositoryMock;

        private List<Brand> sampleData = new List<Brand>
        {
            new Brand
            {
                Name = "Brand1"
            },
            new Brand
            {
                Name = "Brand2"
            },
            new Brand
            {
                Name = "Brand3"
            }
        };

        public BrandServiceTests()
        {
            this.brandRepositoryMock = new Mock<IRepository<Brand>>();;
        }

        [Fact]
        public void AllNamesShouldReturnCorrectStrings()
        {
            brandRepositoryMock
                .Setup(r => r.All())
                .Returns(sampleData.AsQueryable());

            var brandService = new BrandService(brandRepositoryMock.Object);

            var resultNames = brandService.AllNames();

            var expectedResult = sampleData.Select(b => b.Name);

            Assert.Equal(expectedResult, resultNames);
        }

        [Fact]
        public void AllNamesShouldRerurnEmptyCollectionWhenNoBrandsArePresent()
        {
            brandRepositoryMock
                .Setup(r => r.All())
                .Returns(default(IQueryable<Brand>));

            var brandService = new BrandService(brandRepositoryMock.Object);

            var resultNames = brandService.AllNames();

            Assert.Empty(resultNames);
        }

        [Theory]
        [InlineData("Brand1")]
        [InlineData("Brand2")]
        [InlineData("Brand3")]
        public async Task GetBrandByNameAsyncShouldReturnCorrectName(string brandName)
        {
            this.brandRepositoryMock
                .Setup(r => r.All())
                .Returns(this.sampleData.AsQueryable());

            var service = new BrandService(brandRepositoryMock.Object);

            var result = await service.GetBrandByNameAsync(brandName);

            var expectedResult = this.sampleData
                .FirstOrDefault(b => b.Name == brandName);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task GetBrandByNameAsyncShouldReturnNullIfDoesntExist()
        {
            string brandName = "I Dont Exist";

            this.brandRepositoryMock
                .Setup(r => r.All())
                .Returns(default(IQueryable<Brand>));

            var service = new BrandService(brandRepositoryMock.Object);

            var result = await service.GetBrandByNameAsync(brandName);

            Assert.Null(result);
        }
    }
}
