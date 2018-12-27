using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Mapping;
using Vidos.Services.Models.Product.ViewModels;
using Vidos.Web.Common.Exceptions;

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

        public ProductDetailsViewModel GetProductDetailsViewModelById(string id)
        {
            var product = this.GetProductById(id);

            var productModel = Mapper.Map<ProductDetailsViewModel>(product);

            return productModel;
        }

        public AirConditioner GetProductById(string id)
        {
            var product = this._repo.FindById(id);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            return product;
        }

        public async Task AddAsync(AirConditioner product)
        {
            await this._repo.AddAsync(product);

            await this._repo.SaveChangesAsync();
        }
    }
}
