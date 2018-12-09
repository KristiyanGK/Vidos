﻿using System.Collections.Generic;
using System.Linq;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Mapping;
using Vidos.Services.Models.ViewModels;

namespace Vidos.Services.DataServices
{
    public class ProductsService : IProductsService
    {
        private readonly IRepository<AirConditioner> _repo;

        public ProductsService(
            IRepository<AirConditioner> repo)
        {
            this._repo = repo;
        }

        public IEnumerable<AllProductsViewModel> GetAll()
        {
            var list = this._repo.All()
                .To<AllProductsViewModel>()
                .ToList();

            return list;
        }
    }
}
