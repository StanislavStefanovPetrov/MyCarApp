using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCarApp.Common.User.ViewModels;
using MyCarApp.Services.Exceptions;
using MyCarApp.Services.User.Interfaces;
using MyCarApp.Web.Helpers;
using MyCarApp.Web.Helpers.Messages;

namespace MyCarApp.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUserAdvertisementService advertisementService;

        public IndexModel(IUserAdvertisementService advertisementService)
        {
            this.advertisementService = advertisementService;
        }

        public ICollection<IndexAdvertisementAllViewModel> allAdvertisements;

        public async Task<IActionResult> OnGet()
        {
            try
            {
                allAdvertisements = await this.advertisementService.GetAllAdvertisements();
            }
            catch (MyCarAppException ex)
            {
                var msg = new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = ex.Message
                };

                this.TempData.Put("__Message", msg);
            }
            return this.Page();

        }
    }
}
