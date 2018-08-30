using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using MyCarApp.Common.Publisher.BindingModels;
using MyCarApp.Common.Publisher.ViewModels;
using MyCarApp.Common.Utilities;
using MyCarApp.Models;
using MyCarApp.Services.Exceptions;
using MyCarApp.Services.Publisher.Interfaces;
using MyCarApp.Web.Helpers;
using MyCarApp.Web.Helpers.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCarApp.Web.Pages
{
    [Authorize]
    public class AddAdvertisementModel : PageModel
    {
        private readonly IPublisherAdvertisementService advertisementService;
        private readonly IPublisherVehicleService vehicleService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<AddAdvertisementModel> logger;

        public AddAdvertisementModel(IPublisherAdvertisementService advertisementService, IPublisherVehicleService vehicleService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AddAdvertisementModel> logger)
        {
            this.advertisementService = advertisementService;
            this.vehicleService = vehicleService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.Input = new VehicleDataViewModels();
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty(SupportsGet = false)]
        public VehicleDataViewModels Input { get; private set; }

        [BindProperty]
        public AdvertisementBindingModel AdvertisementBindingModel { get; set; }

        [BindProperty]
        public VehicleBindingModel VehicleBindingModel { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                foreach(var value in this.ModelState.Values)
                {
                    foreach(var error in value.Errors)
                    {
                        var msg = new MessageModel()
                        {
                            Type = MessageType.Danger,
                            Message = error.ErrorMessage
                        };

                        this.TempData.Put("__Message", msg);
                    }
                }

                return this.Page();
            }

            var dbUser = await this.userManager.GetUserAsync(this.User);

            if (dbUser == null)
            {
                this.NotFound();
            }

            this.AdvertisementBindingModel.SellerId = dbUser.Id;
            this.AdvertisementBindingModel.SellerUserName = dbUser.Email;
            this.AdvertisementBindingModel.PublishDate = DateTime.Today;

            try
            {
                int vehicleId = await this.vehicleService.CreateNewVehicleAsync(this.VehicleBindingModel, this.AdvertisementBindingModel.SellerId, this.AdvertisementBindingModel.SellerUserName);

                int advertisementId = await this.advertisementService.CreateNewAdvertisement(this.AdvertisementBindingModel, vehicleId);

                this.logger.LogInformation("User created new advertisement.");

                if (!User.IsInRole(AppUtilities.PublisherRole))
                {
                    await this.userManager.AddToRoleAsync(dbUser, AppUtilities.PublisherRole);
                    await this.signInManager.SignInAsync(dbUser, isPersistent: false);
                    await this.userManager.UpdateSecurityStampAsync(dbUser);
                    await this.signInManager.RefreshSignInAsync(dbUser);
                }

                return this.Redirect($"/Publisher/AdvertisementDetails?advId={advertisementId}");
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

        public override async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            this.Input.Colors = await this.vehicleService.GetVehicleColorsAsync();
            this.Input.Engines = await this.vehicleService.GetVehicleEnginesAsync();
            this.Input.FuelTypes = await this.vehicleService.GetVehicleFuelTypesAsync();
            this.Input.Transmissions = await this.vehicleService.GetVehicleTransmissionsAsync();
            this.Input.VehicleConditions = await this.vehicleService.GetVehicleVehicleConditionsAsync();
            this.Input.VehicleMakes = await this.vehicleService.GetVehicleMakesAsync();
            this.Input.VehicleTypes = await this.vehicleService.GetVehicleVehicleVehicleTypesAsync();
        }

        public async Task<IActionResult> OnGetMakeModelsAsync(int makeId)
        {
            var models = await this.vehicleService.GetModelsAsync(makeId);

            var myViewData = new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()) { { "_Index", models } };

            myViewData.Model = models;

            PartialViewResult result = new PartialViewResult()
            {
                ViewName = "_Index",
                ViewData = myViewData,
            };

            return result;
        }
    }
}