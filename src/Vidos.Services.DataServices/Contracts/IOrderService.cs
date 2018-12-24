using System.Linq;
using Vidos.Data.Models;

namespace Vidos.Services.DataServices.Contracts
{
    public interface IOrderService
    {
        IQueryable<Order> Orders { get; }

        void SaveOrder(Order order);
    }
}
