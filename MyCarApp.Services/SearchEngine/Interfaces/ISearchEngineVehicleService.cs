using System.Collections.Generic;
using System.Threading.Tasks;
using MyCarApp.Common.SearchEngine.BindingModels;
using MyCarApp.Common.SearchEngine.ViewModels;
using MyCarApp.Common.User.ViewModels;

namespace MyCarApp.Services.SearchEngine.Interfaces
{
    public interface ISearchEngineVehicleService
    {
        Task<ICollection<VehicleTypeBindingModel>> GetVehicleVehicleVehicleTypesAsync();
        Task<ICollection<VehicleMakeViewModel>> GetVehicleMakesAsync();
        Task<ICollection<ColorBindingModel>> GetVehicleColorsAsync();
        Task<ICollection<FuelTypeBindingModel>> GetVehicleFuelTypesAsync();
        Task<ICollection<TransmissionBindingModel>> GetVehicleTransmissionsAsync();
        Task<ICollection<VehicleConditionBindingModel>> GetVehicleVehicleConditionsAsync();
        Task<ICollection<IndexAdvertisementAllViewModel>> GetSearchResult(SearchEngineVehicleBindingModel vehicleBindingModel);
        Task<ICollection<MyCarApp.Common.Publisher.ViewModels.VehicleModelViewModel>> GetModelsAsync(int makeId);
    }
}
