using System.Collections.Generic;
using System.Threading.Tasks;
using MyCarApp.Common.User.ViewModels;

namespace MyCarApp.Services.Observer.Interfaces
{
    public interface IObserverAdvertisementService
    {
        Task<ICollection<IndexAdvertisementAllViewModel>> GetAllAdvertisements(string userId);
    }
}
