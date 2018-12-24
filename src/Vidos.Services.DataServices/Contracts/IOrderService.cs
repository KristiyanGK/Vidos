using System.Linq;
using System.Threading.Tasks;
using Vidos.Data.Models;

namespace Vidos.Services.DataServices.Contracts
{
    public interface IOrderService
    {
        IQueryable<Order> Orders { get; }

        Task SaveOrderAsync(Order order);
    }
}
