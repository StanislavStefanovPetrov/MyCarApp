using System.Collections.Generic;
using System.Threading.Tasks;
using MyCarApp.Common.User.ViewModels;

namespace MyCarApp.Services.User.Interfaces
{
    public interface IUserAdvertisementService
    {
        Task<ICollection<IndexAdvertisementAllViewModel>> GetAllAdvertisements();

        Task<DetailAdvertisementViewModel> GetAdvertisementDetails(int id);

        Task AddAdvertisementToUserFavorite(int advId, string userId);
    }
}
