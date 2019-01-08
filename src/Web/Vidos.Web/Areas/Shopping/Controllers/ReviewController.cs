using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Mapping;
using Vidos.Services.Models.Reviews.ViewModels;
using Vidos.Web.Common.Constants;
using X.PagedList;

namespace Vidos.Web.Areas.Shopping.Controllers
{
    [Authorize(Roles = Constants.AdminOrUserRole)]
    public class ReviewController : BaseShoppingController
    {
        private readonly IReviewService _reviewService;
        private readonly UserManager<VidosUser> _userManager;

        public ReviewController(IReviewService reviewService,
            UserManager<VidosUser> userManager)
        {
            this._reviewService = reviewService;
            this._userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Create(string productId)
        {
            string currUserId = string.Empty;
            bool isProductReviewed = true;

            await Task.Run(() =>
            {
                currUserId = this._userManager.GetUserId(User);
                isProductReviewed = this._reviewService.HasUserReviewedCurrentProduct(currUserId, productId);
            });

            if (isProductReviewed)
            {
                return RedirectToAction("Details", "Products", new { id = productId });
            }

            this.TempData["ProductId"] = productId;

            return View();
        } 

        [HttpPost]
        public async Task<IActionResult> Create(AddReviewViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var userId = this._userManager.GetUserId(User);

            var review = Mapper.Map<Review>(viewModel);

            var productId = this.TempData["ProductId"].ToString();

            await this._reviewService.AddReviewAsync(review, userId, productId);

            return RedirectToAction("Details", "Products", new {id = productId});
        }

        [HttpGet]
        public async Task<IActionResult> MyReviews()
        {
            List<ListReviewsViewModel> reviews = null;

            await Task.Run(async () =>
            {
                string userId = this._userManager.GetUserId(User);

                reviews = await this._reviewService
                    .GetUserReviewsById(userId)
                    .To<ListReviewsViewModel>()
                    .ToListAsync();
            });

            return View(reviews);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            EditReviewViewModel reviewModel = null;

            await Task.Run(() =>
            {
                Review review = this._reviewService.GetReviewById(id);
                reviewModel = Mapper.Map<EditReviewViewModel>(review);
                this.TempData["ReviewId"] = review.Id;
            });

            return View(reviewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditReviewViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            viewModel.Id = this.TempData["ReviewId"].ToString();

            var review = Mapper.Map<Review>(viewModel);

            await this._reviewService.UpdateReviewAsync(review);

            return RedirectToAction(nameof(MyReviews));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            EditReviewViewModel reviewModel = null;

            await Task.Run(() =>
            {
                Review review = this._reviewService.GetReviewById(id);
                reviewModel = Mapper.Map<EditReviewViewModel>(review);
            });

            return View(reviewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditReviewViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await this._reviewService.RemoveReviewByIdAsync(viewModel.Id);

            return RedirectToAction(nameof(MyReviews));
        }
    }
}
