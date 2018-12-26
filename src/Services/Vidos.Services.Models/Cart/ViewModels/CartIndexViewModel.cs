using System.Collections.Generic;
using Vidos.Services.Models.CartItem.ViewModels;

namespace Vidos.Services.Models.Cart.ViewModels
{
    public class CartIndexViewModel
    {
        public IEnumerable<CartItemIndexViewModel> Items { get; set; }

        public decimal TotalValue { get; set; }

        public string ReturnUrl { get; set; }
    }
}
