using Microsoft.AspNetCore.Mvc;
using Vidos.Services.DataServices.Contracts;

namespace Vidos.Web.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            this._productsService = productsService;
        }

        [HttpGet]
        public IActionResult All()
        {
            var products = this._productsService.GetAll();

            return View(products);
        }
    }
}
