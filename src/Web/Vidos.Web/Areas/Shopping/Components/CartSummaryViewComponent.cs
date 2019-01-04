using Microsoft.AspNetCore.Mvc;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Models.Cart.ViewModels;
using System.Linq;

namespace Vidos.Web.Areas.Shopping.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private ICartService cart;

        public CartSummaryViewComponent(ICartService cartService)
        {
            cart = cartService;
        }

        public IViewComponentResult Invoke()
        {
            CartLayoutViewModel viewModel = new CartLayoutViewModel
            {
                ProductsCount = this.cart.Items.Any() ? this.cart.Items.Sum(c => c.Quantity) : 0,
                TotalValue = this.cart.TotalValue()
            };

            return View(viewModel);
        }
    }
}
