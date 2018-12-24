using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public void SaveOrder(Order order)
        {
            this._ordeRepository.AttachRange(order.Items.Select(l => l.AirConditioner));

            this._ordeRepository.AddAsync(order).GetAwaiter().GetResult();

            this._ordeRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
