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

        public AirConditioner AirConditioner { get; set; }
    }
}
