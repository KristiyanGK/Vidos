using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Mapping;
using Vidos.Services.Models.Product.ViewModels;
using Vidos.Web.Common.Constants;
using Vidos.Web.Common.Exceptions;
using X.PagedList;

namespace Vidos.Web.Areas.Shopping.Controllers
{
    public class ProductsController : BaseShoppingController
    {
        private readonly IProductsService _productsService;

        public ProductsController(
            IProductsService productsService)
        {
            this._productsService = productsService;
        }

        public IActionResult All() => View();

        public async Task<IActionResult> AllPartial(int? pageNumber, string brandName, string priceSort, int productsOnPage)
        {
            var products = await this._productsService
                .GetAllAsync(brandName, priceSort);

            var productModel = products.To<ListProductsViewModel>();

            var page = pageNumber ?? 1;

            var pagedList = productModel.ToPagedList(page, productsOnPage);

            return PartialView("_AllProductsPartial", pagedList);
        }

        [HttpGet]
        [ResponseCache(Duration = Constants.BaseResponsiveCacheDuration,
            Location = ResponseCacheLocation.Client)]
        public IActionResult Details(string id, string query)
        {
            ProductDetailsViewModel product;

            try
            {
                product = Mapper.Map<ProductDetailsViewModel>(this._productsService.GetProductById(id));
            }
            catch (ProductNotFoundException)
            {
                return View("ProductNotFound");
            }

            product.ReturnUrl = "/Shopping" + query;

            return View(product);
        }
    }
}
