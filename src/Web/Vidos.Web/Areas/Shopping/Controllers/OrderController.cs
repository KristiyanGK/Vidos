using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Models.Order.ViewModels;
using Vidos.Web.Common.Constants;
using Vidos.Web.Controllers;

namespace Vidos.Web.Areas.Shopping.Controllers
{
    [Area("Shopping")]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly UserManager<VidosUser> _userManager;

        public OrderController(
            IOrderService orderService,
            ICartService cartService,
            UserManager<VidosUser> userManager)
        {
            _orderService = orderService;
            _cartService = cartService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Checkout() => View();

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderCheckoutViewModel orderCheckoutModel)
        {
            if (!this._cartService.Items.Any())
            {
                ModelState.AddModelError(string.Empty, ErrorMessages.EmptyCart);
            }

            if (ModelState.IsValid)
            {
                var order = Mapper.Map<Order>(orderCheckoutModel);
                var clientId = this._userManager.GetUserId(this.User);

                if (clientId != null)
                {
                    order.ClientId = this._userManager.GetUserId(this.User);
                }

                order.Items = this._cartService.Items.ToArray();

                await this._orderService.SaveOrderAsync(order);

                return RedirectToAction(nameof(Completed));
            }

            return View(orderCheckoutModel);
        }

        public ViewResult Completed()
        {
            this._cartService.Clear();

            return View();
        }
    }
}
