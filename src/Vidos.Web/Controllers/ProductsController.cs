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
        public IActionResult All(int? page)
        {
            var products = this._productsService.GetAll();

            var pageNumber = page ?? 1;
            var onePageOfProducts = products.ToPagedList(pageNumber, 5);

            return View(onePageOfProducts);
        }
    }
}
