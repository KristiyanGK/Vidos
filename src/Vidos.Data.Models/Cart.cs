using System;
using System.Collections.Generic;

namespace Vidos.Data.Models
{
    public class Cart
    {
        public Cart()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CartItems = new HashSet<CartItem>();
        }

        public string Id { get; set; }

        public string PaymentId { get; set; }

        public Payment Payment { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}
