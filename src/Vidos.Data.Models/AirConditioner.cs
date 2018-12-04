using System;
using System.Collections.Generic;

namespace Vidos.Data.Models
{
    public class AirConditioner
    {
        public AirConditioner()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ShoppingCarItems = new HashSet<CartItem>();
        }

        public string Id { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public ICollection<CartItem> ShoppingCarItems { get; set; }
    }
}
