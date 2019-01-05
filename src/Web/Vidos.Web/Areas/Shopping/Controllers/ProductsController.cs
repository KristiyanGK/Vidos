using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Models.Product.ViewModels;
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
            var products = await this._productsService.GetAllAsync(brandName, priceSort);

            var page = pageNumber ?? 1;

            var pagedList = products.ToPagedList(page, productsOnPage);

            return PartialView("_AllProductsPartial", pagedList);
        }

        [HttpGet]
        public IActionResult Details(string id, string query)
        {
            ProductDetailsViewModel product;

            product = this._productsService.GetProductDetailsViewModelById(id); 

            //TODO: CHECK if exists and make not found page

            product.ReturnUrl = "/Shopping" + query;

            return View(product);
        }
    }
}
