using Microsoft.AspNetCore.Authorization;
using Vidos.Web.Common;
using Vidos.Web.Controllers;

namespace Vidos.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = Constants.AdministratorRole)]
    public class BaseAdminController : BaseController
    {

    }
}
