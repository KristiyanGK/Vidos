using Moq;
using System.Collections.Generic;
using System.Linq;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.DataServices;
using Xunit;

namespace Vidos.Services.Tests.ServicesTests
{
    public class BrandServiceTests
    {
        [Fact]
        public void AllNamesShouldReturnCorrectStrings()
        {
            string[] expectedNames =
            {
                "Test1",
                "Test2",
                "Test3"
            };

            var brandRepository = new Mock<IRepository<Brand>>();
            brandRepository
                .Setup(r => r.All())
                .Returns(new List<Brand>()
                {
                    new Brand()
                    {
                        Name = expectedNames[0]
                    },
                    new Brand()
                    {
                        Name = expectedNames[1]
                    },
                    new Brand()
                    {
                        Name = expectedNames[2]
                    }
                }.AsQueryable());

            var brandService = new BrandService(brandRepository.Object);

            var resultNames = brandService.AllNames();

            Assert.Equal(expectedNames, resultNames);
        }
    }
}
