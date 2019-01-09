using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
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
            View(this._orderService.All().To<AllOrdersViewModel>().ToList());

        [HttpPost]
        public async Task<IActionResult> MarkAsShipped(string orderId)
        {
            await this._orderService.MarkOrderAsShippedAsync(orderId);
            
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
