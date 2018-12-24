using System.Collections.Generic;
using Vidos.Data.Models;
using Vidos.Services.Models.ViewModels;

namespace Vidos.Services.DataServices.Contracts
{
    public interface IProductsService
    {
        IEnumerable<AllProductsViewModel> GetAll();
        ProductDetailsViewModel GetProductDetailsViewModelById(string id);
        AirConditioner GetProductById(string id);
    }
}
