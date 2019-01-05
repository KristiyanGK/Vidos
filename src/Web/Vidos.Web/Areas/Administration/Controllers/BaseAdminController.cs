﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vidos.Web.Common.Constants;
using Vidos.Web.Controllers;

namespace Vidos.Web.Areas.Administration.Controllers
{
    [Area("administration")]
    [Authorize(Roles = Constants.AdministratorRole)]
    public class BaseAdminController : BaseController
    {

    }
}
