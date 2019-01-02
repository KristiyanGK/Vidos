using System.Collections.Generic;
using System.Threading.Tasks;
using Vidos.Data.Models;
using Vidos.Services.Models.Product.ViewModels;

namespace Vidos.Services.DataServices.Contracts
{
    public interface IProductsService
    {
        Task<IEnumerable<AllProductsViewModel>> GetAllAsync();
        Task<IEnumerable<AllProductsViewModel>> GetAllAsync(string brandName, string priceSort);
        ProductDetailsViewModel GetProductDetailsViewModelById(string id);
        AirConditioner GetProductById(string id);
        Task AddAsync(AirConditioner product);
    }
}
