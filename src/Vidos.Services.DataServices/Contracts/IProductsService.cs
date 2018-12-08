using System.Collections.Generic;
using Vidos.Services.Models;

namespace Vidos.Services.DataServices.Contracts
{
    public interface IProductsService
    {
        IEnumerable<AllProductsViewModel> GetAll();
    }
}
