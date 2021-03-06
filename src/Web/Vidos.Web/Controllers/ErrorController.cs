﻿using Microsoft.AspNetCore.Mvc;

namespace Vidos.Web.Controllers
{
    public class ErrorController : BaseController
    {
        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }

        [Route("error/{code:int}")]
        public IActionResult Error(int code)
        {
            // handle different codes or just return the default error view
            return View();
        }
    }
}
