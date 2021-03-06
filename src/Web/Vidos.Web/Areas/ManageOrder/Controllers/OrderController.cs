﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Mapping;
using Vidos.Services.Models.Order.ViewModels;
using Vidos.Web.Common.Constants;
using X.PagedList;
using Order = Vidos.Data.Models.Order;

namespace Vidos.Web.Areas.ManageOrder.Controllers
{
    public class OrderController : BaseManagerOrderController
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IProductsService _productsService;
        private readonly UserManager<VidosUser> _userManager;

        public OrderController(
            IOrderService orderService,
            ICartService cartService,
            IProductsService productsService,
            UserManager<VidosUser> userManager)
        {
            _orderService = orderService;
            _cartService = cartService;
            _productsService = productsService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Checkout() => View();

        [HttpPost]
        public async Task<IActionResult> Checkout(
            OrderCheckoutViewModel orderCheckoutModel)
        {
            if (!this._cartService.Items.Any())
            {
                ModelState
                    .AddModelError(
                        string.Empty,
                        ErrorMessages.EmptyCart);
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

                this.TempData["OrderVM"]
                    = JsonConvert.SerializeObject(orderCheckoutModel);

                if (orderCheckoutModel.PaymentMethod == PaymentMethod.Cash)
                { 
                    return RedirectToAction(nameof(CompleteOrder));
                }

                if (orderCheckoutModel.PaymentMethod == PaymentMethod.Card)
                {
                    return RedirectToAction(
                        "Payment",
                        "Payment",
                        new {area = Constants.ManageOrderArea});
                }

                ModelState.AddModelError(
                    string.Empty,
                    ErrorMessages.InvalidPaymentMethod);
            }

            return View(orderCheckoutModel);
        }

        public async Task<IActionResult> CompleteOrder()
        {
            Order order = null;

            await Task.Run(() =>
            {
                OrderCheckoutViewModel orderCheckoutModel
                    = JsonConvert
                        .DeserializeObject<OrderCheckoutViewModel>(
                            this.TempData["OrderVM"].ToString());

                order = Mapper.Map<Order>(orderCheckoutModel);

                order.ClientId = this._userManager.GetUserId(this.User);

                order.Items = this._cartService.Items.ToArray();
            });
            
            await this._orderService.SaveOrderAsync(order);

            return RedirectToAction(nameof(Completed));
        }

        public async Task<IActionResult> Completed()
        {
            var products = this._cartService.Items;

            await this._productsService
                .IncreaseTimesBoughtAllAsync(products);

            this._cartService.Clear();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            string userId = this._userManager.GetUserId(this.User);

            var orders = await (await
                this._orderService.GetClientOrdersById(userId))
                .To<ListOrdersClientViewModel>()
                .ToListAsync();

            return View(orders);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var order = this._orderService.GetAllOrderInfoById(id);

            var orderViewModel = Mapper.Map<OrderDetailsClientViewModel>(order);

            return View(orderViewModel);
        }
    }
}
