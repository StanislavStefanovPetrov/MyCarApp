using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyCarApp.Common.User.ViewModels;
using MyCarApp.Data;
using MyCarApp.Services.Observer.Interfaces;

namespace MyCarApp.Services.Observer
{
    public class ObserverAdvertisementService : BaseEFService, IObserverAdvertisementService
    {
        public ObserverAdvertisementService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ICollection<IndexAdvertisementAllViewModel>> GetAllAdvertisements(string userId)
        {
            var userAdv = await this.DbContext.AdvertisementUsers
                .Where(au => au.UserId == userId)
                .Include(au=>au.Advertisement)
                .Select(au => new IndexAdvertisementAllViewModel()
            {
                Id = au.Advertisement.Id,
                IsExpired=au.Advertisement.IsExpired,
                Title = au.Advertisement.Title,
                PublishDate = au.Advertisement.PublishDate,
                Vehicle = string.Format("{0} - {1} - {2}", au.Advertisement.Vehicle.Make.Name, au.Advertisement.Vehicle.Model.Name, au.Advertisement.Vehicle.FirstRegistration.Year),
                Image = au.Advertisement.Vehicle.PicturePaths.First().Path
            }).ToListAsync();

            return userAdv;
        }
    }
}
