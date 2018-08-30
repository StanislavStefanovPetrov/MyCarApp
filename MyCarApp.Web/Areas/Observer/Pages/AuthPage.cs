
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCarApp.Common.Utilities;

namespace MyCarApp.Web.Areas.Observer.Pages
{
    [Area(AppUtilities.ObserverArea)]
    [Authorize(Roles = AppUtilities.ObserverRole)]
    public abstract class AuthPage : PageModel
    {
    }
}
