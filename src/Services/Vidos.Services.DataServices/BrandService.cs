using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return this._brandRepository.All().Select(b => b.Name).ToList();
        }
    }
}
