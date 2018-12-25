﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Models.Cart.ViewModels;
using Vidos.Web.Controllers;

namespace Vidos.Web.Areas.Shopping.Controllers
{
    [Area("shopping")]
    [Authorize(Roles = "User")]
    public class CartController : BaseController
    {
        private IProductsService _productsService;
        private ICartService _cartService;

        public CartController(
            IProductsService productsService,
            ICartService cartService)
        {
            this._productsService = productsService;
            this._cartService = cartService;
        }

        public IActionResult Index(string returnUrl)
        {
            var model = new CartIndexViewModel()
            {
                Items = this._cartService.Items,
                TotalValue = this._cartService.TotalValue(),
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        public IActionResult AddToCart(string productId, string returnUrl)
        {
            var product = this._productsService.GetProductById(productId);

            if (product != null)
            {
                this._cartService.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public IActionResult RemoveFromCart(string productId, string returnUrl)
        {
            var product = this._productsService.GetProductById(productId);

            if (product != null)
            {
                this._cartService.RemoveById(productId);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}