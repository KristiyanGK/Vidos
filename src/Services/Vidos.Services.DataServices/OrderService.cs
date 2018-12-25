﻿using System.Linq;
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

        public Order GetOrderById(string id)
        {
            Order order = this._ordeRepository.FindById(id);

            return order;
        }

        public IQueryable<Order> All()
        {
            return this._ordeRepository.All();
        }

        public async Task SaveOrderAsync(Order order)
        {
            this._ordeRepository.AttachRange(order.Items.Select(l => l.AirConditioner));

            if (this._ordeRepository.FindById(order.Id) == null)
            {
                await this._ordeRepository.AddAsync(order);
            }

            await this._ordeRepository.SaveChangesAsync();
        }
    }
}