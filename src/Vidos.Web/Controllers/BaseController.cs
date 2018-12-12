using Microsoft.AspNetCore.Mvc;

namespace Vidos.Web.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult RedirectToHome()
        {
            string actionName = nameof(HomeController.Index);

            string controllerName = typeof(HomeController).Name.Substring(0, 4);

            return RedirectToAction(actionName, controllerName);
        }
    }
}