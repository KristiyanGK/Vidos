using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Models.Order.ViewModels;
using Vidos.Web.Controllers;

namespace Vidos.Web.Areas.Shopping.Controllers
{
    [Area("shopping")]
    [Authorize(Roles = "User")]
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
                ModelState.AddModelError("", "Количката Ви е празна!");
            }

            var order = Mapper.Map<Order>(orderCheckoutModel);
            order.ClientId = this._userManager.GetUserId(this.User);

            if (ModelState.IsValid)
            {
                order.Items = this._cartService.Items.ToArray();
                await this._orderService.SaveOrderAsync(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(orderCheckoutModel);
            }
        }

        public ViewResult Completed()
        {
            this._cartService.Clear();
            return View();
        }
    }
}
