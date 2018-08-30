using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCarApp.Common.SearchEngine.BindingModels;
using MyCarApp.Common.SearchEngine.ViewModels;
using MyCarApp.Services.Exceptions;
using MyCarApp.Services.Publisher.Interfaces;
using MyCarApp.Services.SearchEngine.Interfaces;
using MyCarApp.Web.Helpers;
using MyCarApp.Web.Helpers.Messages;
using System.Threading.Tasks;

namespace MyCarApp.Web.Pages
{
    [Authorize]
    public class SearchModel : PageModel
    {
        private readonly ISearchEngineVehicleService vehicleService;

        public SearchModel(IPublisherAdvertisementService advertisementService, ISearchEngineVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
            this.VehicleBindingModel = new SearchEngineVehicleBindingModel();
            this.Input = new VehicleDataViewModels();
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty(SupportsGet = false)]
        public VehicleDataViewModels Input { get; private set; }

        [BindProperty]
        public SearchEngineVehicleBindingModel VehicleBindingModel { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
        }

        public override async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            this.Input.VehicleMakes = await this.vehicleService.GetVehicleMakesAsync();
            this.VehicleBindingModel.Colors = await this.vehicleService.GetVehicleColorsAsync();
            this.VehicleBindingModel.FuelTypes = await this.vehicleService.GetVehicleFuelTypesAsync();
            this.VehicleBindingModel.Transmissions = await this.vehicleService.GetVehicleTransmissionsAsync();
            this.VehicleBindingModel.VehicleConditions = await this.vehicleService.GetVehicleVehicleConditionsAsync();
            this.VehicleBindingModel.VehicleTypes = await this.vehicleService.GetVehicleVehicleVehicleTypesAsync();
        }

        public async Task<IActionResult> OnPost()
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

                return this.Page();
            }

            try
            {
                var searchResult = await this.vehicleService.GetSearchResult(this.VehicleBindingModel);

                this.TempData.Put("___SearchResultViewModel", searchResult);

                return this.RedirectToPage("ShowSearchResult");
            }
            catch (MyCarAppException ex)
            {
                var msg = new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = ex.Message
                };

                this.TempData.Put("__Message", msg);

                return this.Page();
            }
        }
    }
}