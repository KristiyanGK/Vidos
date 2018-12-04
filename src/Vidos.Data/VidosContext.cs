using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vidos.Data.Models;

namespace Vidos.Data
{
    public class VidosContext : IdentityDbContext<VidosUser>
    {
        public VidosContext(DbContextOptions<VidosContext> options)
            : base(options)
        {
        }

        public DbSet<AirConditioner> AirConditioners { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<PaymentType> PaymentTypes { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
