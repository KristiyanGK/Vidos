using System.Collections.Generic;
using Vidos.Data.Models;

namespace Vidos.Services.Models.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<CartItem> Items { get; set; }

        public decimal TotalValue { get; set; }

        public string ReturnUrl { get; set; }
    }
}
