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

        //Returns the order information with included Client and Items data
        public Order GetAllOrderInfoById(string id)
        {
            var order =
                this.All()
                    .Include(o => o.Client)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefault(o => o.Id == id);

            return order;
        }

        public IQueryable<Order> All()
        {
            return this._ordeRepository.All();
        }

        public async Task<Order> SaveOrderAsync(Order order)
        {
            this._ordeRepository.AttachRange(order.Items.Select(l => l.Product));

            if (this._ordeRepository.FindById(order.Id) == null)
            {
                await this._ordeRepository.AddAsync(order);
            }

            await this._ordeRepository.SaveChangesAsync();

            return order;
        }

        public async Task<Order> MarkOrderAsShippedAsync(string orderId)
        {
            var order = this._ordeRepository.FindById(orderId);

            if (order == null)
            {
                return null;
            }

            order.IsShipped = true;

            await this._ordeRepository.SaveChangesAsync();

            return order;
        }

        public async Task<IQueryable<Order>> GetClientOrdersById(string clientId)
        {
            IQueryable<Order> orders = null;

            await Task.Run(() =>
            {
                orders = this._ordeRepository
                    .All()?
                    .Where(o => o.ClientId == clientId);
            });

            return orders;
        }
    }
}
