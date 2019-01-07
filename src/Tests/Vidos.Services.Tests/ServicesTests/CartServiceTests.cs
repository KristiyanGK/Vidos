using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using Vidos.Data.Models;
using Vidos.Services.DataServices;
using Xunit;

namespace Vidos.Services.Tests.ServicesTests
{
    public class CartServiceTests
    {
        private List<AirConditioner> sampleData = new List<AirConditioner>
        {
            new AirConditioner
            {
                Id = "1",
                Price = 100
            },
            new AirConditioner
            {
                Id = "2",
                Price = 200
            },
            new AirConditioner
            {
                Id = "3",
                Price = 300
            }
    };

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        [InlineData(3, 4)]
        [InlineData(4, -123)]
        public void AddItemShouldAddCorrectly(int productIndex, int quantity)
        {
            int productQuantity = quantity;

            if (productQuantity < 0)
            {
                productQuantity = 0;
            }

            AirConditioner sampleProduct = null;

            if (productIndex >= 0 && productIndex < sampleData.Count)
            {
                sampleProduct = sampleData[productIndex];
            }    

            CartItem expectedResult = new CartItem
            {
                Product = sampleProduct,
                Quantity = productQuantity
            };

            var service = new CartService();

            var result = service.AddItem(sampleProduct, quantity);

            bool compare = (expectedResult.Product == result.Product) 
                           && (expectedResult.Quantity == result.Quantity);

            Assert.True(compare);
        }

        [Fact]
        public void ItemsShouldReturnCorrectItems()
        {
            var service = new CartService();
            var expectedResult = new List<CartItem>();

            for (int i = 0; i < sampleData.Count; i++)
            {
                var cartItem = service.AddItem(sampleData[i], i + 1);
                expectedResult.Add(cartItem);
            }

            var result = service.Items.ToList();

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ClearShouldRemoveAllItems()
        {
            var service = new CartService();

            for (int i = 0; i < sampleData.Count; i++)
            {
                service.AddItem(sampleData[i], i + 1);
            }

            service.Clear();

            var result = service.Items;

            Assert.Empty(result);
        }

        [Fact]
        public void TotalValueShouldReturnCorrectDecimal()
        {
            var service = new CartService();

            var expectedResult = sampleData.Sum(p => p.Price);

            for (int i = 0; i < sampleData.Count; i++)
            {
                service.AddItem(sampleData[i], 1);
            }

            var result = service.TotalValue();

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void RemoveByIdShouldRemoveCorrectItem()
        {
            var service = new CartService();
            var expectedResult = new List<CartItem>();

            for (int i = 0; i < sampleData.Count; i++)
            {
                var cartItem = service.AddItem(sampleData[i], i + 1);
                expectedResult.Add(cartItem);
            }

            string idToRemove = sampleData[0].Id;

            service.RemoveById(idToRemove);

            var result = service.Items.ToList();

            expectedResult.RemoveAll(p => p.Product.Id == idToRemove);

            Assert.Equal(expectedResult, result);
        }
    }
}
