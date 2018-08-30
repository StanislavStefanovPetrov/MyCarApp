using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyCarApp.Common.SearchEngine.BindingModels;
using MyCarApp.Common.SearchEngine.ViewModels;
using MyCarApp.Common.User.ViewModels;
using MyCarApp.Data;
using MyCarApp.Models.Vehicles;
using MyCarApp.Services.Exceptions;
using MyCarApp.Services.SearchEngine.Interfaces;
using MyCarApp.Services.Utilities;

namespace MyCarApp.Services.SearchEngine
{
    public class SearchEngineVehicleService : BaseEFService, ISearchEngineVehicleService
    {
        public SearchEngineVehicleService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ICollection<VehicleMakeViewModel>> GetVehicleMakesAsync()
        {
            var models = await this.DbContext
                .VehicleMakes
                .Where(m => m.IsDeleted == false)
                .Select(e => new VehicleMakeViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Models = e.Models.Where(m => m.IsDeleted == false).Select(m => new VehicleModelViewModel()
                    {
                        Id = m.Id,
                        Name = m.Name
                    }).ToList()
                })
                .OrderBy(x => x.Name)
                .ToListAsync();

            models.Insert(0, new VehicleMakeViewModel() { Id = 0, Name = "Any" });

            return models;
        }

        public async Task<ICollection<ColorBindingModel>> GetVehicleColorsAsync()
        {
            var colors = await this.DbContext.Colours.ToListAsync();

            var models = this.Mapper.Map<ICollection<ColorBindingModel>>(colors);

            return models;
        }

        public async Task<ICollection<FuelTypeBindingModel>> GetVehicleFuelTypesAsync()
        {
            var fuelTypes = await this.DbContext.FuelTypes.ToListAsync();

            var models = this.Mapper.Map<ICollection<FuelTypeBindingModel>>(fuelTypes);

            return models;
        }

        public async Task<ICollection<TransmissionBindingModel>> GetVehicleTransmissionsAsync()
        {
            var transmissions = await this.DbContext.Transmissions.ToListAsync();

            var models = this.Mapper.Map<ICollection<TransmissionBindingModel>>(transmissions);

            return models;
        }

        public async Task<ICollection<VehicleTypeBindingModel>> GetVehicleVehicleVehicleTypesAsync()
        {
            var types = await this.DbContext.VehicleTypes.ToListAsync();

            var models = this.Mapper.Map<ICollection<VehicleTypeBindingModel>>(types);

            return models;
        }

        public async Task<ICollection<VehicleConditionBindingModel>> GetVehicleVehicleConditionsAsync()
        {
            var conditions = await this.DbContext.VehicleConditions.ToListAsync();

            var models = this.Mapper.Map<ICollection<VehicleConditionBindingModel>>(conditions);

            return models;
        }

        public async Task<ICollection<IndexAdvertisementAllViewModel>> GetSearchResult(SearchEngineVehicleBindingModel model)
        {
            bool isValid = this.InitialValidationSearchEngineVehicleBindingModel(model);

            if (!isValid) throw new MyCarAppException(Messages.InvalidEntityProperties);

            var vehicles = (IQueryable<Vehicle>) this.DbContext.Vehicles;

            if(model.MakeId!=0)
            {
                vehicles = vehicles.Where(v => v.Make.Id == model.MakeId);
            }

            if (model.ModelId != 0)
            {
                vehicles = vehicles.Where(v => v.Model.Id == model.ModelId);
            }

            if(model.Colors.Any(e=>e.IsChecked))
            {
                vehicles = vehicles.Where(v=>(model.Colors.Where(e => e.IsChecked).Any(c => c.Id == v.VehicleExteriorColour.Id && c.Name == v.VehicleExteriorColour.Name)));
            }

            if (model.FuelTypes.Any(e => e.IsChecked))
            {
                vehicles = vehicles.Where(v => (model.FuelTypes.Where(e => e.IsChecked).Any(ft => ft.Id == v.FuelType.Id && ft.Fuel == v.FuelType.Fuel)));
            }

            if (model.VehicleConditions.Any(e => e.IsChecked))
            {
                vehicles = vehicles.Where(v => (model.VehicleConditions.Where(e => e.IsChecked).Any(vc => vc.Id == v.Condition.Id && vc.Condition == v.Condition.Condition)));
            }

            if (model.VehicleTypes.Any(e => e.IsChecked))
            {
                vehicles = vehicles.Where(v => (model.VehicleTypes.Where(e => e.IsChecked).Any(vt => vt.Id == v.VehicleType.Id && vt.Type == v.VehicleType.Type)));
            }

            if (model.Transmissions.Any(e => e.IsChecked))
            {
                vehicles = vehicles.Where(v => (model.Transmissions.Where(e => e.IsChecked).Any(vt => vt.Id == v.Transmission.Id && vt.Type == v.Transmission.Type)));
            }

            var vehicleIds = await vehicles
                .Where(v =>
                v.Kilometre >= model.MinKilometre &&
                v.Kilometre <= model.MaxKilometre &&
                v.FirstRegistration >= model.FirstRegistrationAfter &&
                v.FirstRegistration <= model.FirstRegistrationBefore &&
                v.Power >= model.MinPower &&
                v.Power <= model.MaxPower &&
                v.Engine.CubicCapacity >= model.MinEngineCapacity &&
                v.Engine.CubicCapacity <= model.MaxEngineCapacity &&
                v.Price >= model.MinPrice &&
                v.Price <= model.MaxPrice
                )
                .Select(v => v.Id)
                .ToListAsync();

            var advertisements = await this.DbContext.Advertisements.Where(a => vehicleIds.Contains(a.VehicleId)).Select(a => new IndexAdvertisementAllViewModel()
            {
                Id = a.Id,
                Title = a.Title,
                IsExpired = a.IsExpired,
                PublishDate = a.PublishDate,
                Vehicle = string.Format("{0} - {1} - {2}", a.Vehicle.Make.Name, a.Vehicle.Model.Name, a.Vehicle.FirstRegistration.Year),
                Image = a.Vehicle.PicturePaths.First().Path
            })
            .ToListAsync();

            return advertisements;
        }

        private bool InitialValidationSearchEngineVehicleBindingModel(SearchEngineVehicleBindingModel model)
        {
            if (model == null) return false;

            if (model.MinPrice > model.MaxPrice) return false;

            if (model.MinEngineCapacity > model.MaxEngineCapacity) return false;

            if (model.MinKilometre > model.MaxKilometre) return false;

            if (model.MinPower > model.MaxPower) return false;

            return true;
        }

        public async Task<ICollection<MyCarApp.Common.Publisher.ViewModels.VehicleModelViewModel>> GetModelsAsync(int makeId)
        {
            var vehicleModels = await this.DbContext
                .VehicleModels
                .Where(m => m.IsDeleted == false && m.MakeId == makeId)
                .OrderBy(m => m.Id).ToListAsync();

            vehicleModels.Insert(0, new VehicleModel() { Id = 0, Name = "Any" });

            var models = this.Mapper.Map<ICollection<MyCarApp.Common.Publisher.ViewModels.VehicleModelViewModel>>(vehicleModels);

            return models;
        }
    }
}
