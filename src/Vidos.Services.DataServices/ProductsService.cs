﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

        public ProductDetailsViewModel GetProductById(string id)
        {
            var product = this._repo.FindById(id);

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            
            var productModel = Mapper.Map<ProductDetailsViewModel>(product);

            return productModel;
        }
    }
}