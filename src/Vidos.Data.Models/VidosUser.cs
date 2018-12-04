using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Vidos.Data.Models
{
    public class VidosUser : IdentityUser
    {
        public VidosUser()
        {
            this.PaymentHistory = new HashSet<Payment>();
        }

        public string Address { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Payment> PaymentHistory { get; set; }
    }
}
