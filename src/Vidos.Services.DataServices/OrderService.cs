using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;

namespace Vidos.Services.DataServices
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _ordeRepository;

        public OrderService(IRepository<Order> ordeRepository)
        {
            this._ordeRepository = ordeRepository;
        }

        public IQueryable<Order> Orders =>
            this._ordeRepository.All()
                .Include(o => o.Items)
                .ThenInclude(l => l.AirConditioner);

        public async Task SaveOrderAsync(Order order)
        {
            this._ordeRepository.AttachRange(order.Items.Select(l => l.AirConditioner));

            await this._ordeRepository.AddAsync(order);

            await this._ordeRepository.SaveChangesAsync();
        }
    }
}
