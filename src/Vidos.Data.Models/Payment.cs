using System;

namespace Vidos.Data.Models
{
    public class Payment
    {
        public Payment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public decimal Amount { get; set; }

        public string CustomerId { get; set; }

        public VidosUser Customer { get; set; }

        public string PaymentTypeId { get; set; }

        public PaymentType Type { get; set; }
    }
}
