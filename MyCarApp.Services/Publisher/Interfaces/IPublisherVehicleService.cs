using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyCarApp.Common.Publisher.BindingModels;
using MyCarApp.Common.Publisher.ViewModels;

namespace MyCarApp.Services.Publisher.Interfaces
{
    public interface IPublisherVehicleService
    {
        Task<ICollection<ColorViewModel>> GetVehicleColorsAsync();
        Task<ICollection<EngineViewModel>> GetVehicleEnginesAsync();
        Task<ICollection<FuelTypeViewModel>> GetVehicleFuelTypesAsync();
        Task<ICollection<TransmissionViewModel>> GetVehicleTransmissionsAsync();
        Task<ICollection<VehicleConditionViewModel>> GetVehicleVehicleConditionsAsync();
        Task<ICollection<VehicleModelViewModel>> GetModelsAsync(int makeId);
        Task<ICollection<VehicleMakeViewModel>> GetVehicleMakesAsync();
        Task<ICollection<VehicleTypeViewModel>> GetVehicleVehicleVehicleTypesAsync();
        Task<int> CreateNewVehicleAsync(VehicleBindingModel bindingModel, string userId, string username);
        Task AdvertisementAddNewImage(IFormFile pictureFile, string userId, string fullName, int vehicleId);
    }
}
