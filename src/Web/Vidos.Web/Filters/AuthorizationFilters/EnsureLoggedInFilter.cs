using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Vidos.Data.Models;
using Vidos.Web.Common.Constants;

namespace Vidos.Web.Filters.AuthorizationFilters
{
    public class EnsureLoggedInFilter : IAsyncAuthorizationFilter
    {
        private readonly UserManager<VidosUser> _userManager;
        private readonly SignInManager<VidosUser> _signInManager;

        public EnsureLoggedInFilter(
            UserManager<VidosUser> userManager, 
            SignInManager<VidosUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!_signInManager.IsSignedIn(context.HttpContext.User))
            {
                string userGuid = Guid.NewGuid().ToString();

                VidosUser guest = new VidosUser
                {
                    UserName = "guest_" + userGuid,
                    FirstName = Constants.GuestName,
                    LastName = userGuid
                };

                await this._userManager.CreateAsync(guest);

                await this._userManager.AddToRoleAsync(guest, Constants.GuestRole);

                await this._signInManager.SignInAsync(guest, true);
            }
        }
    }
}
