using Microsoft.AspNetCore.Builder;

namespace Vidos.Web.Middlewares
{
    public static class RolesWithAdminSeederExtension
    {
        public static IApplicationBuilder UseRolesWithAdminSeeder(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedRolesWithAdminMiddleware>();
        }
    }
}
