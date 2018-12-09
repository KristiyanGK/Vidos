using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Models;

namespace Vidos.Services.DataServices
{
    public class ProductsService : IProductsService
    {
        private readonly IRepository<AirConditioner> _repo;
        private readonly IMapper _autoMapper;

        public ProductsService(
            IRepository<AirConditioner> repo,
            IMapper autoMapper)
        {
            this._repo = repo;
            this._autoMapper = autoMapper;
        }

        public IEnumerable<AllProductsViewModel> GetAll()
        {
            var list = this._repo.All()
                .Select(x => this._autoMapper.Map<AllProductsViewModel>(x))
                .ToList();

            return list;
        }
    }
}
