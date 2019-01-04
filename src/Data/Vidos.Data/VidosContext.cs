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

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
