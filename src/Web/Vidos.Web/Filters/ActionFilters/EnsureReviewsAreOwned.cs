using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Stripe;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Models.Reviews.ViewModels;

namespace Vidos.Web.Filters.ActionFilters
{
    public class EnsureReviewsAreOwned : IAsyncActionFilter
    {
        private readonly IReviewService _reviewService;
        private readonly UserManager<VidosUser> _userManager;

        public EnsureReviewsAreOwned(
            IReviewService reviewService,
            UserManager<VidosUser> userManager)
        {
            this._reviewService = reviewService;
            this._userManager = userManager;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var routeData = context.ActionArguments;

            string reviewId = string.Empty;

            if (routeData.ContainsKey("id"))
            {
                reviewId = routeData["id"].ToString();
            }
            else if(routeData.ContainsKey("viewModel"))
            {
                reviewId = ((EditReviewViewModel) routeData["viewModel"]).Id;
            }

            string userId = this._userManager.GetUserId(context.HttpContext.User);

            bool result = this._reviewService.IsReviewOwnedByUser(userId, reviewId);

            if (!result)
            {
                context.Result = new RedirectToActionResult("MyReviews", "Review", new {});
            }
            else
            {
                var resultContext = await next();
            }   
        }
    }
}
