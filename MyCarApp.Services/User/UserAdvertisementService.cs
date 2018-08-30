using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyCarApp.Common.User.ViewModels;
using MyCarApp.Data;
using MyCarApp.Models.Advertisements;
using MyCarApp.Services.Exceptions;
using MyCarApp.Services.User.Interfaces;
using MyCarApp.Services.Utilities;

namespace MyCarApp.Services.User
{
    public class UserAdvertisementService : BaseEFService, IUserAdvertisementService
    {
        public UserAdvertisementService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task AddAdvertisementToUserFavorite(int advId, string userId)
        {         

            var advertisement = this.DbContext.Advertisements.Include(a=>a.Watchers).First(a=>a.Id==advId);

            if (advertisement.IsExpired)
            {
                throw new MyCarAppException(Messages.AdvIsExpiredAlready);
            }

            if (advertisement.SellerId==userId)
            {
                throw new MyCarAppException(Messages.AdvSellerCannotAdd);
            }

            if (advertisement.Watchers.Any(au => au.UserId == userId ))
            {
                throw new MyCarAppException(Messages.AdvAlreadyAddedToUser);
            }

            var advUser = new AdvertisementUser() { UserId = userId, Advertisement = advertisement };

            this.DbContext.AdvertisementUsers.Add(advUser);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task<DetailAdvertisementViewModel> GetAdvertisementDetails(int id)
        {
            var advertisement = await this.DbContext.Advertisements.Where(a => a.Id==id).Select(a => new DetailAdvertisementViewModel()
            {
                Id=a.Id,
                Title = a.Title,
                IsExpired=a.IsExpired,
                PublishDate = a.PublishDate,
                Seller = new DetailAdvertisementSellerViewModel()
                {
                    Email=a.Seller.Email,
                    FullName=a.Seller.FullName
                },
                Vehicle = new DetailAdvertisementVehicleViewModel()
                {
                    Type=a.Vehicle.VehicleType.Type,
                    Condition=a.Vehicle.Condition.Condition,
                    Make=a.Vehicle.Make.Name,
                    Model=a.Vehicle.Model.Name,
                    Transmission=a.Vehicle.Transmission.Type,
                    FuelType=a.Vehicle.FuelType.Fuel,
                    Kilometre=a.Vehicle.Kilometre,
                    Power=a.Vehicle.Power,
                    Price=a.Vehicle.Price,
                    EngineCubicCapacity=a.Vehicle.Engine.CubicCapacity,
                    FirstRegistration=a.Vehicle.FirstRegistration,
                    VehicleExteriorColour=a.Vehicle.VehicleExteriorColour.Name,
                    Images=a.Vehicle.PicturePaths.Select(p=>p.Path).ToList()
                }
            }).FirstOrDefaultAsync();

            if(advertisement==null)
            {
                throw new MyCarAppException(Messages.AdvDoesNoExists);
            }
            return advertisement;
        }

        public async Task<ICollection<IndexAdvertisementAllViewModel>> GetAllAdvertisements()
        {
            var userAdv = await this.DbContext.Advertisements.Where(a => !a.IsExpired).Select(a => new IndexAdvertisementAllViewModel()
            {
                Id = a.Id,
                Title = a.Title,
                IsExpired=a.IsExpired,
                PublishDate = a.PublishDate,
                Vehicle = string.Format("{0} - {1} - {2}", a.Vehicle.Make.Name, a.Vehicle.Model.Name, a.Vehicle.FirstRegistration.Year),
                Image=a.Vehicle.PicturePaths.First().Path
            }).ToListAsync();

            return userAdv;
        }
    }
}
