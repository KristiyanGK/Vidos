﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Threading.Tasks;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Web.Common.Constants;

namespace Vidos.Web.Areas.ManageOrder.Controllers
{
    public class PaymentController : BaseManagerOrderController
    {
        private readonly UserManager<VidosUser> _userManager;
        private readonly ICartService _cartService;
        private readonly CustomerService _customerService;
        private readonly ChargeService _chargeService;

        public PaymentController(UserManager<VidosUser> userManager, ICartService cartService,
            CustomerService customerService, ChargeService chargeService)
        {
            this._userManager = userManager;
            this._cartService = cartService;
            this._customerService = customerService;
            this._chargeService = chargeService;
        }

        [HttpGet]
        public IActionResult Payment() => View();

        [HttpPost]
        public async Task<IActionResult> Charge(string stripeToken)
        {
            var user = await this._userManager.GetUserAsync(User);

            string email = await this._userManager.GetEmailAsync(user);

            var customer = this._customerService.Create(new CustomerCreateOptions
            {
                Email = email,
                SourceToken = stripeToken
            });

            /*
             * Converts charge ammount to the lowest value in the currency
             * (with levs it is cents) and rounds the number to the higher one
             */
            var amount = (long)Math.Ceiling(this._cartService.TotalValue() 
                                            * Constants.CentsInLev);

            var charge = this._chargeService.Create(new ChargeCreateOptions
            {
                Amount = amount,
                Description = Constants.ChargeDescription,
                Currency = Constants.CurrancyType,
                CustomerId = customer.Id
            });

            return RedirectToAction("CompleteOrder", "Order");
        }
    }
}
