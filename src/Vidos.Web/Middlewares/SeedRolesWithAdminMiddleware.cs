using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Vidos.Data.Models;
using Vidos.Web.Configurations;

namespace Vidos.Web.Middlewares
{
    public class SeedRolesWithAdminMiddleware
    {
        private readonly RequestDelegate next;

        public SeedRolesWithAdminMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, RoleManager<IdentityRole> roleManager, UserManager<VidosUser> userManager)
        {
            var adminRoleExists = await roleManager.RoleExistsAsync(Constants.AdministratorRole);
            if (!adminRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = Constants.AdministratorRole });
            }

            var userRoleExists = (await roleManager.RoleExistsAsync(Constants.UserRole));
            if (!userRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = Constants.UserRole });
            }

            var adminUserExists = await userManager.FindByNameAsync(Constants.AdministratorName) != null;
            if (!adminUserExists)
            {
                var user = new VidosUser
                {
                    UserName = Constants.AdministratorName,
                    Email = Constants.AdministratorEmail,
                    FirstName = Constants.AdministratorName,
                    LastName = Constants.AdministratorName,
                    Address = Constants.AdministratorName
                };

                await userManager.CreateAsync(user, Constants.AdministratorPassword);
                await userManager.AddToRoleAsync(user, Constants.AdministratorRole);
            }

            await this.next(context);
        }
    }
}
