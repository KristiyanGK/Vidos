using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Models.Brand.ViewModels;
using Vidos.Web.Common.Constants;

namespace Vidos.Web.Areas.Shopping.Controllers
{
    [ResponseCache(Duration = Constants.BaseResponsiveCacheDuration,
        Location = ResponseCacheLocation.Client)]
    public class BrandController : BaseShoppingController
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public IActionResult AllNames()
            => new JsonResult(_brandService.AllNames());

        [ResponseCache(Duration = Constants.BaseResponsiveCacheDuration,
            Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> Details(string name)
        {
            var brand = await this._brandService.GetBrandByNameAsync(name);

            if (brand == null)
            {
                return View("BrandNotFound");
            }

            var viewModel = Mapper.Map<BrandDetailsViewModel>(brand);

            return View(viewModel);
        }
    }
}
