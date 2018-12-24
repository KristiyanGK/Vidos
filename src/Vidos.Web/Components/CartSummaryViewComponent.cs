using Microsoft.AspNetCore.Mvc;
using Vidos.Services.DataServices.Contracts;

namespace Vidos.Web.Components
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
            return View(cart);
        }
    }
}
