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
using MyCarApp.Models.Vehicles;
using MyCarApp.Services.Exceptions;
using MyCarApp.Services.Publisher.Interfaces;
using MyCarApp.Services.Utilities;

namespace MyCarApp.Services.Publisher
{
    public class PublisherVehicleService : BaseEFService, IPublisherVehicleService
    {
        private readonly IPictureService pictureService;

        public PublisherVehicleService(
               ApplicationDbContext dbContext,
               IMapper mapper, IPictureService pictureService)
               : base(dbContext, mapper)
        {
            this.pictureService = pictureService;
        }

        public async Task<int> CreateNewVehicleAsync(VehicleBindingModel bindingModel, string userId, string username)
        {
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    VehicleType type = this.GetVehicleType(bindingModel);

                    VehicleCondition condition = this.GetVehicleCondition(bindingModel);

                    VehicleMake make = this.GetVehicleMake(bindingModel);

                    VehicleModel model = this.GetVehicleModel(bindingModel, make);

                    Colour color = this.GetVehicleColor(bindingModel);

                    Transmission transmission = this.GetVehicleTransmission(bindingModel);

                    FuelType fuelType = this.GerVehicleFuelType(bindingModel);

                    Engine engine = this.GetVehicleEngine(bindingModel, fuelType);

                    var vehicle = new Vehicle()
                    {
                        VehicleType = type,
                        Condition = condition,
                        Make = make,
                        Model = model,
                        FirstRegistration = bindingModel.FirstRegistration,
                        Kilometre = bindingModel.Kilometre,
                        Price = bindingModel.Price,
                        Power = bindingModel.Power,
                        Engine = engine,
                        VehicleExteriorColour = color,
                        Transmission = transmission,
                        FuelType=fuelType
                    };

                    this.DbContext.Vehicles.Add(vehicle);
                    await this.DbContext.SaveChangesAsync();

                    PicturePath picturePath = await this.pictureService.GetNewPicturePath(bindingModel.PictureFile, userId, username, vehicle.Id);

                    vehicle.PicturePaths.Add(picturePath);
                    await this.DbContext.SaveChangesAsync();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();

                    return vehicle.Id;
                }
                catch (Exception ex)
                {
                    throw new MyCarAppException(ex.Message);
                }
            }
        }
        
        public async Task<ICollection<VehicleModelViewModel>> GetModelsAsync(int makeId)
        {
            var vehicleModels = await this.DbContext
                .VehicleModels
                .Where(m => m.IsDeleted == false && m.MakeId == makeId)
                .OrderBy(m=>m.Id).ToListAsync();

            var models = this.Mapper.Map<ICollection<VehicleModelViewModel>>(vehicleModels);

            return models;
        }

        public async Task<ICollection<ColorViewModel>> GetVehicleColorsAsync()
        {
            var colors = await this.DbContext.Colours.ToListAsync();

            var models = this.Mapper.Map<ICollection<ColorViewModel>>(colors);

            return models;
        }

        public async Task<ICollection<EngineViewModel>> GetVehicleEnginesAsync()
        {
            var engines = await this.DbContext.Engines.ToListAsync();

            var models = this.Mapper.Map<ICollection<EngineViewModel>>(engines);

            return models;
        }

        public async Task<ICollection<FuelTypeViewModel>> GetVehicleFuelTypesAsync()
        {
            var fuelTypes = await this.DbContext.FuelTypes.ToListAsync();

            var models = this.Mapper.Map<ICollection<FuelTypeViewModel>>(fuelTypes);

            return models;
        }

        public async Task<ICollection<TransmissionViewModel>> GetVehicleTransmissionsAsync()
        {
            var transmissions = await this.DbContext.Transmissions.ToListAsync();

            var models = this.Mapper.Map<ICollection<TransmissionViewModel>>(transmissions);

            return models;
        }

        public async Task<ICollection<VehicleConditionViewModel>> GetVehicleVehicleConditionsAsync()
        {
            var conditions = await this.DbContext.VehicleConditions.ToListAsync();

            var models = this.Mapper.Map<ICollection<VehicleConditionViewModel>>(conditions);

            return models;
        }

        public async Task<ICollection<VehicleMakeViewModel>> GetVehicleMakesAsync()
        {
            var makes = await this.DbContext
                .VehicleMakes
                .Where(m => m.IsDeleted == false)
                .OrderBy(x => x.Name).ToListAsync();

            var models = this.Mapper.Map<ICollection<VehicleMakeViewModel>>(makes);

            return models;
        }

        public async Task<ICollection<VehicleTypeViewModel>> GetVehicleVehicleVehicleTypesAsync()
        {
            var types = await this.DbContext.VehicleTypes.ToListAsync();

            var models = this.Mapper.Map<ICollection<VehicleTypeViewModel>>(types);

            return models;
        }

        public async Task AdvertisementAddNewImage(IFormFile pictureFile, string userId, string fullName, int vehicleId)
        {
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    var vehicle = this.DbContext.Vehicles.Find(vehicleId);

                    PicturePath picturePath = await this.pictureService.GetNewPicturePath(pictureFile, userId, fullName, vehicleId);
                    
                    vehicle.PicturePaths.Add(picturePath);
                    await this.DbContext.SaveChangesAsync();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    throw new MyCarAppException(ex.Message);
                }
            }
        }

        private Engine GetVehicleEngine(VehicleBindingModel bindingModel, FuelType fuelType)
        {
            int capacity = bindingModel.EngineCapacity;
            var engine = this.GetEngineByCapability(capacity);
            if (engine == null)
            {
                engine = new Engine() { CubicCapacity = capacity, };
                engine.FuelTypes.Add(new EngineFuelType() { FuelType = fuelType });
            }
            else
            {
                if (!engine.FuelTypes.Any(e => e.FuelType.Fuel == fuelType.Fuel))
                {
                    engine.FuelTypes.Add(new EngineFuelType() { FuelType = fuelType });
                }
            }

            return engine;
        }

        private FuelType GerVehicleFuelType(VehicleBindingModel bindingModel)
        {
            int fuelTypeId = bindingModel.FuelTypeId;
            var fuelType = this.GetFuelTypeById(fuelTypeId);
            if (fuelType == null)
            {
                throw new MyCarAppException(string.Format(Messages.EntityDoesNoExists, "fuelType"));
            }

            return fuelType;
        }

        private Transmission GetVehicleTransmission(VehicleBindingModel bindingModel)
        {
            int transmissionId = bindingModel.TransmissionId;
            var transmission = this.GetTransmissionById(transmissionId);
            if (transmission == null)
            {
                throw new MyCarAppException(string.Format(Messages.EntityDoesNoExists, "transmission"));
            }

            return transmission;
        }

        private Colour GetVehicleColor(VehicleBindingModel bindingModel)
        {
            int colorId = bindingModel.ColorId;
            var color = this.GetColorById(colorId);
            if (color == null)
            {
                throw new MyCarAppException(string.Format(Messages.EntityDoesNoExists, "color"));
            }

            return color;
        }

        private VehicleModel GetVehicleModel(VehicleBindingModel bindingModel, VehicleMake make)
        {
            int modelId = bindingModel.ModelId;
            var model = this.GetModelById(modelId);
            if (model == null)
            {
                throw new MyCarAppException(string.Format(Messages.EntityDoesNoExists, "model"));
            }
            else if (model.MakeId != make.Id)
            {
                throw new MyCarAppException(string.Format(Messages.MakeModelDiscrepancy, make.Name, model.Name));
            }

            return model;
        }

        private VehicleMake GetVehicleMake(VehicleBindingModel bindingModel)
        {
            int makeId = bindingModel.MakeId;
            var make = this.GetMakeById(makeId);
            if (make == null)
            {
                throw new MyCarAppException(string.Format(Messages.EntityDoesNoExists, "make"));
            }

            return make;
        }

        private VehicleCondition GetVehicleCondition(VehicleBindingModel bindingModel)
        {
            int conditionlId = bindingModel.ConditionIds;
            var condition = this.GetConditionById(conditionlId);
            if (condition == null)
            {
                throw new MyCarAppException(string.Format(Messages.EntityDoesNoExists, "condition"));
            }

            return condition;
        }

        private VehicleType GetVehicleType(VehicleBindingModel bindingModel)
        {
            int typeId = bindingModel.TypeId;
            var type = this.GetTypeById(typeId);
            if (type == null)
            {
                throw new MyCarAppException(string.Format(Messages.EntityDoesNoExists, "type"));
            }

            return type;
        }

        private Engine GetEngineByCapability(int capacity)
        {
            return this.DbContext.Engines.Include(e=>e.FuelTypes).FirstOrDefault(e => e.CubicCapacity == capacity);
        }

        private FuelType GetFuelTypeById(int id)
        {
            return this.DbContext.FuelTypes.FirstOrDefault(e => e.Id == id);
        }

        private Transmission GetTransmissionById(int id)
        {
            return this.DbContext.Transmissions.FirstOrDefault(e => e.Id == id);
        }

        private Colour GetColorById(int id)
        {
            return this.DbContext.Colours.FirstOrDefault(e => e.Id == id);
        }

        private VehicleModel GetModelById(int id)
        {
            return this.DbContext.VehicleModels.Where(e => !e.IsDeleted).FirstOrDefault(e => e.Id == id);
        }

        private VehicleMake GetMakeById(int id)
        {
            return this.DbContext.VehicleMakes.Where(e => !e.IsDeleted).FirstOrDefault(e => e.Id == id);
        }

        private VehicleCondition GetConditionById(int id)
        {
            return this.DbContext.VehicleConditions.FirstOrDefault(e => e.Id == id);
        }

        private VehicleType GetTypeById(int id)
        {
            return this.DbContext.VehicleTypes.FirstOrDefault(e => e.Id == id);
        }
       
    }
}
