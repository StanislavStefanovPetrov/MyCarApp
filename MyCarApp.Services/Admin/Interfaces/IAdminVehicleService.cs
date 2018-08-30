using MyCarApp.Common.Admin.BindingModels;
using MyCarApp.Common.Admin.ViewModels;
using MyCarApp.Models.Vehicles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCarApp.Services.Admin.Interfaces
{
    public interface IAdminVehicleService
    {
        Task<IEnumerable<VehicleMakeConciseViewModel>> GetMakesAsync();

        Task<TModel> GetViewMakeAsync<TModel>(int id);

        Task<int> AddMakeAsync(VehicleMakeCreationBindingModel model);

        Task DeleteMakeAsync(int id);

        Task<int> AddMakeModelAsync(VehicleModelCreatingBindingModel model);

        Task<int> EditModelAsync(VehicleModelEdittingBindingModel model);

        Task<TModel> GetViewModelAsync<TModel>(int id);

        Task<int> DeleteModelAsync(int id);
    }
}
