using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyCarApp.Web.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Application description page.";
        }
    }
}
