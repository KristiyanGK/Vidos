using System;

namespace Vidos.Data.Models
{
    public class CartItem
    {
        public CartItem()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public int Quantity { get; set; }

        public decimal TotalCost { get; set; }

        public string AirConditionerId { get; set; }

        public AirConditioner AirConditioner { get; set; }

        public string ShoppingCartId { get; set; }

        public Cart ShoppingCart { get; set; }
    }
}
