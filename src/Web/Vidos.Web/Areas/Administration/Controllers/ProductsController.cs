using System.Reflection.Metadata;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Models.Product.ViewModels;
using Vidos.Web.Common.Constants;

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
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ProductsCreateViewModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return View(productModel);
            }

            var result = await this._productsService.AddAsync(productModel);

            if (result == null)
            {
                return View(productModel);
            }

            string productId = result.Id;

            return RedirectToAction("Details", "Products", new { area = Constants.ShoppingArea, id = productId });
        }
    }
}
