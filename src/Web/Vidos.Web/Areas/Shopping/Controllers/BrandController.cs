using Microsoft.AspNetCore.Mvc;
using Vidos.Services.DataServices.Contracts;
using Vidos.Web.Controllers;

namespace Vidos.Web.Areas.Shopping.Controllers
{
    [Area("Shopping")]
    public class BrandController : BaseController
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public IActionResult All()
        {
            var all = _brandService.AllNames();

            return Json(all);
        }
    }
}
