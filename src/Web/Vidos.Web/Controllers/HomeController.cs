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
            var mostBoughtProducts = await _productsService
                .MostBoughtProducts(Constants.HomeIndexProductCount)
                .To<ListProductsViewModel>()
                .ToListAsync();

            return View(mostBoughtProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
