using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.DataServices.Extensions;
using Vidos.Services.Models.Product.ViewModels;
using Vidos.Web.Common.Exceptions;

namespace Vidos.Services.DataServices
{
    public class ProductsService : IProductsService
    {
        private readonly IRepository<AirConditioner> _repo;
        private readonly IBrandService _brandService;

        public ProductsService(
            IRepository<AirConditioner> repo,
            IBrandService brandService)
        {
            this._repo = repo;
            this._brandService = brandService;
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
            var product = this._repo
                .All()
                .Include(p => p.Brand)
                .Include(p => p.Reviews)
                .ThenInclude(r => r.Client)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            return product;
        }

        public async Task<AirConditioner> AddAsync(ProductsCreateViewModel productModel)
        {
            var brandName = productModel?.BrandName;

            var brand = await this._brandService.GetBrandByNameAsync(brandName);

            if (brand == null)
            {
                return null;
            }

            var product = Mapper.Map<AirConditioner>(productModel);

            product.BrandId = brand.Id;

            var newlyAddedProduct = await this._repo.AddAsync(product);

            await this._repo.SaveChangesAsync();

            return newlyAddedProduct;
        }

        /*returns the products with the highest TimesBought*/
        public async Task<IQueryable<AirConditioner>> MostBoughtProductsAsync(int count)
        {
            IQueryable<AirConditioner> result = null;

            await Task.Run(() =>
            {
                result = this._repo
                    .All()
                    .OrderBy(p => p.TimesBought)
                    .Take(count);
            });

            return result;
        }

        public async Task<AirConditioner> IncreaseTimesBoughtAsync(string productId, int count)
        {
            AirConditioner product = null;

            if (count < 0)
            {
                return null;
            }
            
            await Task.Run(() =>
            {
               product = this._repo.FindById(productId);
            });

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            product.TimesBought += count;

            await this._repo.SaveChangesAsync();

            return product;
        }
    }
}
