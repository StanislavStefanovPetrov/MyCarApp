using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCarApp.Common.User.ViewModels;
using MyCarApp.Web.Helpers;
using System.Collections.Generic;

namespace MyCarApp.Web.Pages
{
    [Authorize]
    public class ShowSearchResultModel : PageModel
    {
        public ShowSearchResultModel()
        {
        }

        public ICollection<IndexAdvertisementAllViewModel> allAdvertisements;

        public IActionResult OnGet()
        {
            if (allAdvertisements == null)
            {
                return this.LocalRedirect("/Search");
            }

            return this.Page();
        }

        public override void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            allAdvertisements = this.TempData.Get<ICollection<IndexAdvertisementAllViewModel>>("___SearchResultViewModel");
        }
    }
}