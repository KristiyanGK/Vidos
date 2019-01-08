using System.Linq;
using System.Threading.Tasks;
using Vidos.Data.Models;
using Vidos.Services.Models.Product.ViewModels;

namespace Vidos.Services.DataServices.Contracts
{
    public interface IProductsService
    {
        Task<IQueryable<AirConditioner>> GetAllAsync(string brandName, string priceSort);

        AirConditioner GetProductById(string id);

        Task<AirConditioner> AddAsync(ProductsCreateViewModel productModel);

        IQueryable<AirConditioner> MostBoughtProducts(int count);  
    }
}
