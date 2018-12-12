using System.Collections.Generic;
using Vidos.Services.Models.ViewModels;

namespace Vidos.Services.DataServices.Contracts
{
    public interface IProductsService
    {
        IEnumerable<AllProductsViewModel> GetAll();
        ProductDetailsViewModel GetProductById(string id);
    }
}
