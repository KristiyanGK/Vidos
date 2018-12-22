using System;
using System.Collections.Generic;

namespace Vidos.Data.Models
{
    public class PaymentType
    {
        public PaymentType()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Payments = new HashSet<Payment>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<Payment> Payments { get; set; }

    }
}
