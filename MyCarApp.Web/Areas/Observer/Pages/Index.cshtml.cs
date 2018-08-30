using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCarApp.Common.User.ViewModels;
using MyCarApp.Models;
using MyCarApp.Services.Exceptions;
using MyCarApp.Services.Observer.Interfaces;
using MyCarApp.Web.Helpers;
using MyCarApp.Web.Helpers.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCarApp.Web.Areas.Observer.Pages
{
    public class IndexModel : AuthPage
    {
        private readonly IObserverAdvertisementService advertisementService;
        private readonly UserManager<ApplicationUser> userManager;

        public IndexModel(IObserverAdvertisementService advertisementService, UserManager<ApplicationUser> userManager)
        {
            this.advertisementService = advertisementService;
            this.userManager = userManager;
        }

        public ICollection<IndexAdvertisementAllViewModel> allAdvertisements;

        public async Task<IActionResult> OnGet()
        {
            var dbUser = await this.userManager.GetUserAsync(this.User);

            if (dbUser == null)
            {
                this.NotFound();
            }

            try
            {
                allAdvertisements = await this.advertisementService.GetAllAdvertisements(dbUser.Id);
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