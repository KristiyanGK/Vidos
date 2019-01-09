using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Mapping;
using Vidos.Services.Models.Product.ViewModels;
using Vidos.Web.Common.Constants;
using X.PagedList;

namespace Vidos.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductsService _productsService;

        public HomeController(IProductsService productsService)
        {
            this._productsService = productsService;
        }

        public async Task<IActionResult> Index()
        {
            var mostBoughtProducts = await (await _productsService
                .MostBoughtProductsAsync(Constants.HomeIndexProductCount))
                .To<ListProductsViewModel>()
                .ToListAsync();

            return View(mostBoughtProducts);
        }

        [ResponseCache(Duration = Constants.BaseResponsiveCacheDuration,
            Location = ResponseCacheLocation.Client)]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = Constants.BaseResponsiveCacheDuration,
            Location = ResponseCacheLocation.Client)]
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = Constants.BaseResponsiveCacheDuration,
            Location = ResponseCacheLocation.Client)]
        public IActionResult Contact()
        {
            return View();
        }
    }
}
