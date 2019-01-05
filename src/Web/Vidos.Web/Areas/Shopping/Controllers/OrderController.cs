using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Stripe;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Models.Order.ViewModels;
using Vidos.Web.Common.Constants;
using Vidos.Web.Controllers;
using Order = Vidos.Data.Models.Order;

namespace Vidos.Web.Areas.Shopping.Controllers
{
    public class OrderController : BaseShoppingController
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
                if (this.User.IsInRole(Constants.GuestRole))
                {
                    var user = await this._userManager.GetUserAsync(User);

                    user.FirstName = orderCheckoutModel.FirstName;
                    user.LastName = orderCheckoutModel.LastName;
                    user.Email = orderCheckoutModel.Email;

                    await this._userManager.UpdateAsync(user);
                }

                if (orderCheckoutModel.PaymentMethod == PaymentMethod.Cash)
                {
                    this.TempData["OrderVM"] = JsonConvert.SerializeObject(orderCheckoutModel);

                    return RedirectToAction(nameof(CompleteOrder), orderCheckoutModel);
                }
                else if (orderCheckoutModel.PaymentMethod == PaymentMethod.Card)
                {
                    this.TempData["OrderVM"] = JsonConvert.SerializeObject(orderCheckoutModel);

                    return RedirectToAction("Payment", "Payment");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ErrorMessages.InvalidPaymentMethod);
                }
            }

            return View(orderCheckoutModel);
        }

        public async Task<IActionResult> CompleteOrder()
        {
            OrderCheckoutViewModel orderCheckoutModel = JsonConvert.DeserializeObject<OrderCheckoutViewModel>(this.TempData["OrderVM"].ToString());

            var order = Mapper.Map<Order>(orderCheckoutModel);

            order.ClientId = this._userManager.GetUserId(this.User);

            order.Items = this._cartService.Items.ToArray();

            await this._orderService.SaveOrderAsync(order);

            return RedirectToAction(nameof(Completed));
        }

        public IActionResult Completed()
        {
            this._cartService.Clear();

            return View();
        }
    }
}
