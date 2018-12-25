using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Models.Product.ViewModels;

namespace Vidos.Web.Areas.Administration.Controllers
{
    public class ProductsController : BaseAdminController
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductsCreateViewModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return View(productModel);
            }

            var product = Mapper.Map<AirConditioner>(productModel);

            string productId = product.Id;

            await this._productsService.AddAsync(product);

            return RedirectToAction("Details", "Products", new object[] { productId });
        }
    }
}
