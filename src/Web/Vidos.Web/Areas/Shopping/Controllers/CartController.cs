using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Mapping;
using Vidos.Services.Models.Cart.ViewModels;
using Vidos.Services.Models.CartItem.ViewModels;

namespace Vidos.Web.Areas.Shopping.Controllers
{
    public class CartController : BaseShoppingController
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
                Items = this._cartService.Items.AsQueryable().To<CartItemIndexViewModel>().ToList(),
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
