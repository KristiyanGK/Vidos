using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vidos.Common;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.Models.ViewModels;

namespace Vidos.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = Constants.AdministratorRole)]
    public class ProductsAdminController : BaseAdminController
    {
        private readonly IRepository<AirConditioner> _productsRepository;

        public ProductsAdminController(
            IRepository<AirConditioner> productsRepository)
        {
            this._productsRepository = productsRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductCreationViewModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return View(productModel);
            }

            var product = Mapper.Map<AirConditioner>(productModel);

            string productId = product.Id;

            await this._productsRepository.AddAsync(product);

            await this._productsRepository.SaveChangesAsync();

            return RedirectToAction("Details", "Products", new object[] { productId });
        }
    }
}
