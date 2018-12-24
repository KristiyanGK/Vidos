using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Vidos.Data.Models
{
    public class VidosUser : IdentityUser
    {
        public VidosUser()
        {
            this.OrderHistory = new HashSet<Order>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Order> OrderHistory { get; set; }
    }
}
