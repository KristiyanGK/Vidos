using Microsoft.AspNetCore.Mvc;
using Vidos.Services.DataServices.Contracts;
using X.PagedList;

namespace Vidos.Web.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductsService _productsService;

        public ProductsController(
            IProductsService productsService)
        {
            this._productsService = productsService;
        }

        [HttpGet]
        public IActionResult All(int? pageNumber)
        {
            var products = this._productsService.GetAll();

            var page = pageNumber ?? 1;
            var onePageOfProducts = products.ToPagedList(page, 9);

            return View(onePageOfProducts);
        }

        [HttpGet]
        public IActionResult Details(string id, string returnUrl)
        {
            var product = this._productsService.GetProductDetailsViewModelById(id);

            product.ReturnUrl = returnUrl;

            return View(product);
        }
    }
}
