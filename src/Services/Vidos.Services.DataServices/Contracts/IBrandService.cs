using System.Collections.Generic;
using System.Threading.Tasks;
using Vidos.Data.Models;

namespace Vidos.Services.DataServices.Contracts
{
    public interface IBrandService
    {
        IEnumerable<string> AllNames();

        Task<Brand> GetBrandByNameAsync(string name);
    }
}
