using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Vidos.Data.Models
{
    public class VidosUser : IdentityUser
    {
        public VidosUser()
        {
            this.OrderHistory = new HashSet<Order>();
            this.Reviews = new HashSet<Review>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Order> OrderHistory { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
