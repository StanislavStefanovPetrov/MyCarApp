
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCarApp.Common.Utilities;

namespace MyCarApp.Web.Areas.Publisher.Pages
{
    [Area(AppUtilities.PublisherArea)]
    [Authorize(Roles = AppUtilities.PublisherRole)]
    public abstract class AuthPage : PageModel
    {
    }
}
