using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Mapping;
using Vidos.Services.Models.Order.ViewModels;

namespace Vidos.Web.Areas.Administration.Controllers
{
    public class OrderController : BaseAdminController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult All() =>
            View(this._orderService.All().To<AllOrdersViewModel>());

        [HttpPost]
        public IActionResult MarkAsShipped(string orderId)
        {
            Order order = this._orderService.GetOrderById(orderId);
            
            if (order != null)
            {
                order.IsShipped = true;
                this._orderService.SaveOrderAsync(order);
            }

            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(string id)
        {
            var order = this._orderService.GetAllOrderInfoById(id);

            var orderDetailsModel = Mapper.Map<OrderDetailsViewModel>(order);

            return View(orderDetailsModel);
        }
    }
}
