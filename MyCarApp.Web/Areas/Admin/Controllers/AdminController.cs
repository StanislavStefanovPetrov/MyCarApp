using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCarApp.Common.Utilities;

namespace MyCarApp.Web.Areas.Admin.Controllers
{
    [Area(AppUtilities.AdministratorArea)]
    [Authorize(Roles = AppUtilities.AdministratorRole)]
    public abstract class AdminController : Controller
    {
    }
}