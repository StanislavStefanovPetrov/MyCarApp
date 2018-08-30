using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyCarApp.Common.Publisher.BindingModels;
using MyCarApp.Common.Publisher.ViewModels;
using MyCarApp.Data;
using MyCarApp.Data.Migrations;
using MyCarApp.Models.Advertisements;
using MyCarApp.Models.Vehicles;
using MyCarApp.Services.Exceptions;
using MyCarApp.Services.Publisher.Interfaces;
using MyCarApp.Services.Utilities;

namespace MyCarApp.Services.Publisher
{
    public class PublisherAdvertisementService : BaseEFService, IPublisherAdvertisementService
    {
        public PublisherAdvertisementService(
               ApplicationDbContext dbContext,
               IMapper mapper)
               : base(dbContext, mapper)
        {
        }

        

        public async Task<int> CreateNewAdvertisement(AdvertisementBindingModel advertisementBindingModel, int vehicleId)
        {
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    var advertisement = new Advertisement()
                    {
                        Title = advertisementBindingModel.Title,
                        SellerId = advertisementBindingModel.SellerId,
                        PublishDate = advertisementBindingModel.PublishDate,
                        VehicleId = vehicleId,
                    };

                    this.DbContext.Advertisements.Add(advertisement);
                    await this.DbContext.SaveChangesAsync();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();

                    return advertisement.Id;
                }
                catch (Exception ex)
                {
                    throw new MyCarAppException(ex.Message);
                }
            }
        }

        public void GetAdvertisement(string userId, int advId)
        {
            var advertisement = this.DbContext.Advertisements.Find(advId);

            if (advertisement.SellerId != userId)
            {
                throw new MyCarAppException(Messages.AdvUserIsNotSeller);
            }

            if (advertisement.IsExpired)
            {
                throw new MyCarAppException(Messages.AdvIsExpiredAlready);
            }
        }

        public async Task<UserAdvertisementDetailViewModel> GetUserAdvertisement(string userId, int advId)
        {
            var advertisement = await this.DbContext
                .Advertisements
                .Where(a => a.Id == advId)
                .Select(a=> new UserAdvertisementDetailViewModel
                {
                    Id=a.Id,
                    SellerId=a.SellerId,
                    Title=a.Title,
                    PublishDate=a.PublishDate,
                    IsExpired=a.IsExpired,
                    Vehicle=new UserAdvertisementVehicleViewModel()
                    {
                        Type=a.Vehicle.VehicleType.Type,
                        Condition=a.Vehicle.Condition.Condition,
                        Make=a.Vehicle.Make.Name,
                        Model=a.Vehicle.Model.Name,
                        FirstRegistration=a.Vehicle.FirstRegistration,
                        Kilometre=a.Vehicle.Kilometre,
                        Price=a.Vehicle.Price,
                        Power=a.Vehicle.Power,
                        EngineCubicCapacity=a.Vehicle.Engine.CubicCapacity,
                        FuelType=a.Vehicle.FuelType.Fuel,
                        VehicleExteriorColour=a.Vehicle.VehicleExteriorColour.Name,
                        Images= a.Vehicle.PicturePaths.Select(p=>new UserAdvertisementVehicleImageViewModel() {
                            Path=p.Path
                        }).ToList(),
                        Transmission=a.Vehicle.Transmission.Type
                    },
                    Watchers=a.Watchers.Select(w=> new UserAdvertisementWatcher()
                    {
                        Email=w.User.Email,
                        FullName=w.User.FullName
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if(advertisement==null)
            {
                throw new MyCarAppException(Messages.AdvDoesNoExists);
            }

            if(advertisement.SellerId!=userId)
            {
                throw new MyCarAppException(Messages.AdvDenyAccess);
            }

            return advertisement;
        }

        public async Task<ICollection<UserAdvertisementAllViewModel>> GetUserAllAdvertisements(string userId)
        {
            var userAdv = await this.DbContext.Advertisements.Where(a => a.SellerId == userId).Select(a=>new UserAdvertisementAllViewModel() {
                Id=a.Id,
                Title=a.Title,
                PublishDate=a.PublishDate,
                Vehicle=string.Format("{0} - {1} - {2}", a.Vehicle.Make.Name , a.Vehicle.Model.Name,a.Vehicle.FirstRegistration.Year),
                IsExpired=a.IsExpired,
                Watchers=a.Watchers.Count
            }).ToListAsync();

            return userAdv;
        }

        public int GetVehicleId(int advId)
        {
            var advertisement = this.DbContext.Advertisements.Find(advId);

            return advertisement.VehicleId;
        }

        public async Task SetAdvToExpired(string userId, int advId)
        {
            var advertisement = this.DbContext.Advertisements.Find(advId);

            if(advertisement.SellerId!=userId)
            {
                throw new MyCarAppException(Messages.AdvUserIsNotSeller);
            }

            if(advertisement.IsExpired)
            {
                throw new MyCarAppException(Messages.AdvIsExpiredAlready);
            }
            advertisement.IsExpired = true;
            await this.DbContext.SaveChangesAsync();
        }
    }
}
