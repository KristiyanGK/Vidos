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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
