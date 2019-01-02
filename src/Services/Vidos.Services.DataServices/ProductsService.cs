using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.DataServices.Extensions;
using Vidos.Services.Mapping;
using Vidos.Services.Models.Product.ViewModels;
using Vidos.Web.Common.Constants;
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


        public async Task<IEnumerable<AllProductsViewModel>> GetAllAsync()
        {
            var list = await this._repo.All()
                .Include(p => p.Brand)
                .To<AllProductsViewModel>()
                .ToListAsync();

            return list;
        }

        public async Task<IEnumerable<AllProductsViewModel>> GetAllAsync(string brandName, string priceSort)
        {
            var list = await this._repo.All()
                .Include(p => p.Brand)
                .ApplyFilters(brandName, priceSort)
                .To<AllProductsViewModel>()
                .ToListAsync();
            
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
            var product = this._repo.All().Include(p => p.Brand).FirstOrDefault(p => p.Id == id);

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
