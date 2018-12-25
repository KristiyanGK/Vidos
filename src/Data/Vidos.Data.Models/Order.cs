using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vidos.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PurchaseDate = DateTime.UtcNow;
            this.Items = new HashSet<CartItem>();
        }

        public string Id { get; set; }

        public string ClientId { get; set; }

        public VidosUser Client { get; set; }

        [Required]
        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        public string Zip { get; set; }

        [Required]
        public string Country { get; set; }

        public bool IsShipped { get; set; }

        public DateTime PurchaseDate { get; set; }

        public ICollection<CartItem> Items { get; set; }
    }
}
