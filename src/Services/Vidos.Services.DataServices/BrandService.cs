using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;

namespace Vidos.Services.DataServices
{
    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> _brandRepository;

        public BrandService(IRepository<Brand> brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public IEnumerable<string> AllNames()
        {
            IEnumerable<string> result =
                this._brandRepository.All()?.Select(b => b.Name).ToList();

            if (result == null)
            {
                return new List<string>();
            }

            return result;
        }

        public async Task<Brand> GetBrandByNameAsync(string name)
        {
            Brand brand = null;

            await Task.Run(() =>
            {
                brand = this._brandRepository.All()?
                    .FirstOrDefault(b => b.Name == name);
            });

            return brand;
        }
    }
}
