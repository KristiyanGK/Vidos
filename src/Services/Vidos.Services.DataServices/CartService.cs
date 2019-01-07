using System.Collections.Generic;
using System.Linq;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;

namespace Vidos.Services.DataServices
{
    public class CartService : ICartService
    {
        private HashSet<CartItem> items;

        public CartService()
        {
            this.items = new HashSet<CartItem>();
        }

        public virtual CartItem AddItem(AirConditioner product, int quantity)
        {
            CartItem item = items
                .FirstOrDefault(p => p.Product.Id == product.Id);

            if (quantity < 0)
            {
                quantity = 0;
            }

            if (item == null)
            {
                item = new CartItem()
                {
                    Product = product,
                    Quantity = quantity
                };

                items.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }

            return item;
        }

        public virtual void RemoveById(string productId) =>
            this.items.RemoveWhere(p => p.Product.Id == productId);

        public virtual decimal TotalValue() =>
            this.items.Sum(p => p.Product.Price * p.Quantity);

        public virtual void Clear() =>
            this.items.Clear();

        public virtual IEnumerable<CartItem> Items =>
            this.items;
    }
}