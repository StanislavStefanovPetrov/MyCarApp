using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyCarApp.Common.User.ViewModels;
using MyCarApp.Common.Utilities;
using MyCarApp.Models;
using MyCarApp.Services.Exceptions;
using MyCarApp.Services.User.Interfaces;
using MyCarApp.Web.Helpers;
using MyCarApp.Web.Helpers.Messages;
using System.Threading.Tasks;

namespace MyCarApp.Web.Pages
{
    [Route("Home/AdvertisementDetails/{id:int}")]
    [Authorize]
    public class AdvertisementDetailsModel : PageModel
    {
        private readonly IUserAdvertisementService advertisementService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<AdvertisementDetailsModel> logger;

        public AdvertisementDetailsModel(IUserAdvertisementService advertisementService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AdvertisementDetailsModel> logger)
        {
            this.advertisementService = advertisementService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        public DetailAdvertisementViewModel advertisement;

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                advertisement = await this.advertisementService.GetAdvertisementDetails(id);
            }
            catch (MyCarAppException ex)
            {
                var msg = new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = ex.Message
                };

                this.TempData.Put("__Message", msg);

                return this.RedirectToPage("./Index");
            }
            return this.Page();
        }

        public async Task<IActionResult> OnPostAddToFavorite(int id)
        {
            var dbUser = await this.userManager.GetUserAsync(this.User);

            if (dbUser == null)
            {
                this.NotFound();
            }

            try
            {
                advertisement = await this.advertisementService.GetAdvertisementDetails(id);

                await this.advertisementService.AddAdvertisementToUserFavorite(advertisement.Id, dbUser.Id);
            }
            catch (MyCarAppException ex)
            {
                var msg = new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = ex.Message
                };

                this.TempData.Put("__Message", msg);

                return this.RedirectToPage("./Index");
            }

            this.logger.LogInformation($"User added successfully new advertisement with id: {id} to his favorite.");

            if (!User.IsInRole(AppUtilities.ObserverRole))
            {
                await this.userManager.AddToRoleAsync(dbUser, AppUtilities.ObserverRole);
                await this.signInManager.SignInAsync(dbUser, isPersistent: false);
                await this.userManager.UpdateSecurityStampAsync(dbUser);
                await this.signInManager.RefreshSignInAsync(dbUser);
            }

            var msgSuccess = new MessageModel()
            {
                Type = MessageType.Success,
                Message = "Advertisement successfully added to your favorite."
            };

            this.TempData.Put("__Message", msgSuccess);

            return this.Page();
        }
    }
}