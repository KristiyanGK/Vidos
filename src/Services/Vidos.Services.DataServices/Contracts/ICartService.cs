using System.Collections.Generic;
using System.Linq;
using Vidos.Data.Models;

namespace Vidos.Services.DataServices.Contracts
{
    public interface ICartService
    {
        CartItem AddItem(AirConditioner product, int quantity);

        void RemoveById(string productId);

        decimal TotalValue();

        void Clear();

        IEnumerable<CartItem> Items { get; }
    }
}