using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.DataServices;
using Xunit;

namespace Vidos.Services.Tests.ServicesTests
{
    public class OrderServiceTests
    {
        private Mock<IRepository<Order>> _orderRepositoryMock;

        private List<Order> sampleOrders = new List<Order>
        {
            new Order
            {
                Id = "1"
            },
            new Order
            {
                Id = "2"
            }
        };

        public OrderServiceTests()
        {
            this._orderRepositoryMock = new Mock<IRepository<Order>>();
        }

        [Fact]
        public async Task MarkOrderAsShippedAsyncShouldUpdateOrderCorrectly()
        {
            string orderId = "1";

            var sampleOrder = new Order
            {
                Id = orderId,
                IsShipped = false
            };

            this._orderRepositoryMock
                .Setup(r => r.FindById(orderId))
                .Returns(sampleOrder);

            this._orderRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.FromResult(1));

            var service = new OrderService(this._orderRepositoryMock.Object);

            var shippedOrder = await service.MarkOrderAsShippedAsync(orderId);

            Assert.True(shippedOrder.IsShipped);
        }

        [Fact]
        public async Task MarkOrderAsShippedAsyncShouldReturnNullOnInvalidId()
        {
            string orderId = "1";

            this._orderRepositoryMock
                .Setup(r => r.FindById(orderId))
                .Returns(default(Order));
        
            var service = new OrderService(this._orderRepositoryMock.Object);

            var shippedOrder = await service.MarkOrderAsShippedAsync(orderId);

            Assert.Null(shippedOrder);
        }

        [Fact]
        public void AllShouldReturnCorrectOrders()
        {
            _orderRepositoryMock
                .Setup(r => r.All())
                .Returns(sampleOrders.AsQueryable());

            var service = new OrderService(this._orderRepositoryMock.Object);

            var result = service.All();

            Assert.Equal(sampleOrders.AsQueryable(), result);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("999")]
        public void GetOrderByIdShouldReturnCorrectOrder(string orderId)
        {
            this._orderRepositoryMock
                .Setup(r => r.FindById(orderId))
                .Returns(sampleOrders.FirstOrDefault(o => o.Id == orderId));

            var service = new OrderService(this._orderRepositoryMock.Object);

            var result = service.GetOrderById(orderId);

            var expectedResult = sampleOrders.FirstOrDefault(o => o.Id == orderId);

            Assert.Equal(expectedResult, result);
        }
    }
}
