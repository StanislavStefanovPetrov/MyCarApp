using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCarApp.Models;
using MyCarApp.Services.Exceptions;
using MyCarApp.Services.Publisher.Interfaces;
using MyCarApp.Web.Helpers;
using MyCarApp.Web.Helpers.Messages;
using System.Threading.Tasks;

namespace MyCarApp.Web.Areas.Publisher.Pages
{
    public class AdvertisementExpiredModel : AuthPage
    {
        private readonly IPublisherAdvertisementService advertisementService;
        private readonly UserManager<ApplicationUser> userManager;

        public int AdvId { get; set; }

        public AdvertisementExpiredModel(IPublisherAdvertisementService advertisementService, UserManager<ApplicationUser> userManager)
        {
            this.advertisementService = advertisementService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnGet(int advId)
        {
            var dbUser = await this.userManager.GetUserAsync(this.User);

            if (dbUser == null)
            {
                this.NotFound();
            }

            try
            {
                this.advertisementService.GetAdvertisement(dbUser.Id, advId);
            }
            catch (MyCarAppException ex)
            {
                var msg = new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = ex.Message
                };

                this.TempData.Put("__Message", msg);

                return this.RedirectToPage("/All");

            }

            this.AdvId = advId;

            return this.Page();
        }
        
        public async Task<IActionResult> OnPost(int advId)
        {
            var dbUser = await this.userManager.GetUserAsync(this.User);

            if (dbUser == null)
            {
                this.NotFound();
            }

            try
            {
                await this.advertisementService.SetAdvToExpired(dbUser.Id, advId);
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

            return this.RedirectToPage("/All");
        }
    }
}