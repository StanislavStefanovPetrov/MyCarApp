using System.Collections.Generic;
using System.Threading.Tasks;
using MyCarApp.Common.Publisher.BindingModels;
using MyCarApp.Common.Publisher.ViewModels;

namespace MyCarApp.Services.Publisher.Interfaces
{
    public interface IPublisherAdvertisementService
    {
        Task<int> CreateNewAdvertisement(AdvertisementBindingModel advertisementBindingModel, int vehicleId);

        Task<ICollection<UserAdvertisementAllViewModel>> GetUserAllAdvertisements(string userId);

        Task<UserAdvertisementDetailViewModel> GetUserAdvertisement(string userId, int advId);

        int GetVehicleId(int advId);

        Task SetAdvToExpired(string userId, int advId);

        void GetAdvertisement(string userId, int advId);
    }
}
