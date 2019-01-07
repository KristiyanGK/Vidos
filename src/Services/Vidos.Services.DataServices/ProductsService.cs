using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.DataServices.Extensions;
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

        /*
         * PriceSort -> 0 for none, 1 for ascending, 2 for descending
         */
        public async Task<IQueryable<AirConditioner>> GetAllAsync(string brandName, string priceSort)
        {
            var list = await Task.Run(() => 
                this._repo.All()
                .Include(p => p.Brand)
                .ApplyFilters(brandName, priceSort));
            
            return list;
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

        public async Task<AirConditioner> AddAsync(AirConditioner product)
        {
            var newlyAddedProduct = await this._repo.AddAsync(product);

            await this._repo.SaveChangesAsync();

            return newlyAddedProduct;
        }

        /*returns the products with the higehst TimesBought*/
        public IQueryable<AirConditioner> MostBoughtProducts(int count)
        {
            var result = this._repo.All().OrderBy(p => p.TimesBought).Take(count);

            return result;
        }
    }
}
