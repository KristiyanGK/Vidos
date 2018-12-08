using System;
using System.Collections.Generic;
using System.Text;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Models;

namespace Vidos.Services.DataServices
{
    public class ProductsService : IProductsService
    {
        private readonly IRepository<AirConditioner> _repo;

        public ProductsService(IRepository<AirConditioner> repo)
        {
            this._repo = repo;
        }

        public IEnumerable<AllProductsViewModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
