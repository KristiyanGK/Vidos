using System.Linq;
using System.Threading.Tasks;
using Vidos.Data.Models;

namespace Vidos.Services.DataServices.Contracts
{
    public interface IProductsService
    {
        Task<IQueryable<AirConditioner>> GetAllAsync(string brandName, string priceSort);
        AirConditioner GetProductById(string id);
        Task<AirConditioner> AddAsync(AirConditioner product);
        IQueryable<AirConditioner> MostBoughtProducts(int count);
    }
}
