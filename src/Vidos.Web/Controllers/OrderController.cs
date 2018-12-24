using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Models.ViewModels;

namespace Vidos.Web.Controllers
{
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
        public IActionResult Checkout(OrderViewModel orderModel)
        {
            if (!this._cartService.Items.Any())
            {
                ModelState.AddModelError("", "Количката Ви е празна!");
            }

            var order = Mapper.Map<Order>(orderModel);
            order.ClientId = this._userManager.GetUserId(this.User);

            if (ModelState.IsValid)
            {
                order.Items = this._cartService.Items.ToArray();
                this._orderService.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(orderModel);
            }
        }

        public ViewResult Completed()
        {
            this._cartService.Clear();
            return View();
        }
    }
}
