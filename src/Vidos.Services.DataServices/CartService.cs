using System.Collections.Generic;
using System.Linq;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Models;

namespace Vidos.Services.DataServices
{
    public class CartService : ICartService
    {
        private List<CartItem> items;

        public CartService()
        {
            this.items = new List<CartItem>();
        }

        public virtual void AddItem(AirConditioner product, int quantity)
        {
            CartItem item = items
                .FirstOrDefault(p => p.AirConditioner.Id == product.Id);

            if (item == null)
            {
                items.Add(new CartItem
                {
                    AirConditioner = product,
                    Quantity = quantity
                });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public virtual void RemoveById(string productId) =>
            this.items.RemoveAll(p => p.AirConditioner.Id == productId);

        public virtual decimal TotalValue() =>
            this.items.Sum(p => p.AirConditioner.Price * p.Quantity);

        public virtual void Clear() =>
            this.items.Clear();

        public virtual IEnumerable<CartItem> Items =>
            this.items;
    }
}
