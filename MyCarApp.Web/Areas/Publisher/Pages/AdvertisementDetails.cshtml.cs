using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MyCarApp.Common.Publisher.ViewModels;
using MyCarApp.Models;
using MyCarApp.Services.Exceptions;
using MyCarApp.Services.Publisher.Interfaces;
using MyCarApp.Web.Helpers;
using MyCarApp.Web.Helpers.Messages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MyCarApp.Web.Areas.Publisher.Pages
{
    public class AdvertisementDetailsModel : AuthPage
    {
        private readonly IPublisherAdvertisementService advertisementService;
        private readonly IPublisherVehicleService vehicleService;
        private readonly UserManager<ApplicationUser> userManager;

        public AdvertisementDetailsModel(IPublisherAdvertisementService advertisementService,IPublisherVehicleService vehicleService ,UserManager<ApplicationUser> userManager)
        {
            this.advertisementService = advertisementService;
            this.vehicleService = vehicleService;
            this.userManager = userManager;
        }

        public UserAdvertisementDetailViewModel advertisement;

        public bool UserIsSeller { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Image")]
        public IFormFile PictureFile { get; set; }

        public async Task<IActionResult> OnGet(int advId)
        {
            var dbUser = await this.userManager.GetUserAsync(this.User);

            if (dbUser == null)
            {
                return this.NotFound();
            }

            try
            {
                advertisement = await this.advertisementService.GetUserAdvertisement(dbUser.Id, advId);

                this.UserIsSeller= advertisement.SellerId == dbUser.Id;
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
            return this.Page();
        }

        public async Task<IActionResult> OnGetAddNewImageAsync(int Id)
        {
            var dbUser = await this.userManager.GetUserAsync(this.User);

            if (dbUser == null)
            {
                this.NotFound();
            }

            try
            {
                advertisement = await this.advertisementService.GetUserAdvertisement(dbUser.Id, Id);
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

            var myViewData = new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()) { { "_AddNewImage", this } };

            myViewData.Model = this;

            PartialViewResult result = new PartialViewResult()
            {
                ViewName = "_AddNewImage",
                ViewData = myViewData,
            };

            return result;
        }
        
        public async Task<IActionResult> OnPostAddNewImage(int Id)
        {
            if (!this.ModelState.IsValid)
            {
                foreach (var value in this.ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        var msg = new MessageModel()
                        {
                            Type = MessageType.Danger,
                            Message = error.ErrorMessage
                        };

                        this.TempData.Put("__Message", msg);
                    }
                }

                return this.RedirectToPage("AdvertisementDetails", new  { advId=Id});
            }
            var dbUser = await this.userManager.GetUserAsync(this.User);

            if (dbUser == null)
            {
                return this.NotFound();
            }

            try
            {
                advertisement = await this.advertisementService.GetUserAdvertisement(dbUser.Id, Id);

                var vehicleId = this.advertisementService.GetVehicleId(Id);

                await this.vehicleService.AdvertisementAddNewImage(this.PictureFile,dbUser.Id,dbUser.UserName,vehicleId);

                var msg = new MessageModel()
                {
                    Type = MessageType.Success,
                    Message = "Image succesfully added."
                };

                this.TempData.Put("__Message", msg);
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
            return this.Page();
        }
    }
}