using System;
using System.Collections.Generic;

namespace Vidos.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Items = new HashSet<CartItem>();
        }

        public string Id { get; set; }

        public string ClientId { get; set; }

        public VidosUser Client { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Country { get; set; }

        public ICollection<CartItem> Items { get; set; }
    }
}
