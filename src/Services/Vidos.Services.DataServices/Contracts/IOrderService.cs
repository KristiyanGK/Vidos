using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidos.Data.Models;

namespace Vidos.Services.DataServices.Contracts
{
    public interface IOrderService
    {
        IQueryable<Order> All();

        Task<Order> SaveOrderAsync(Order order);

        Order GetAllOrderInfoById(string id);

        Task<Order> MarkOrderAsShippedAsync(string orderId);

        Task<IQueryable<Order>> GetClientOrdersById(string clientId);
    }
}