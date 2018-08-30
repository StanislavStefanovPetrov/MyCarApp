using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyCarApp.Common.Admin.BindingModels;
using MyCarApp.Common.Admin.ViewModels;
using MyCarApp.Data;
using MyCarApp.Models.Vehicles;
using MyCarApp.Services;
using MyCarApp.Services.Admin.Interfaces;
using MyCarApp.Services.Exceptions;
using MyCarApp.Services.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftUniClone.Services.Admin
{
    public class AdminVehicleService : BaseEFService, IAdminVehicleService
    {

        public AdminVehicleService(
            ApplicationDbContext dbContext,
            IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<IEnumerable<VehicleMakeConciseViewModel>> GetMakesAsync()
        {
            var makes = await this.DbContext.VehicleMakes
                .Where(vm => vm.IsDeleted == false)
                .OrderBy(x => x.Name)
                .Include(e => e.Models)
                .Select(x => new VehicleMake()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Models = x.Models.Where(m => m.IsDeleted == false).OrderBy(m => m.Name).ToList(),
                }).ToListAsync();
            var modelMakes = this.Mapper.Map<IEnumerable<VehicleMakeConciseViewModel>>(makes);
            return modelMakes;
        }

        private async Task<VehicleMake> GetMakeAsync(int id)
        {
            var make = await this.DbContext.VehicleMakes
                   .Where(vm => vm.IsDeleted == false)
                   .Include(e => e.Models)
                   .Select(x => new VehicleMake()
                   {
                       Id = x.Id,
                       Name = x.Name,
                       Models = x.Models.Where(m => m.IsDeleted == false).OrderBy(m => m.Name).ToList(),
                   })
                .FirstOrDefaultAsync(c => c.Id == id);

            return make;
        }

        private async Task<VehicleMake> GetMakeAsync(string name)
        {
            var make = await this.DbContext.VehicleMakes
                   .Where(vm => vm.IsDeleted == false)
                   .Include(e => e.Models)
                   .Select(x => new VehicleMake()
                   {
                       Id = x.Id,
                       Name = x.Name,
                       Models = x.Models.Where(m => m.IsDeleted == false).OrderBy(m => m.Name).ToList(),
                   })
                .FirstOrDefaultAsync(c => c.Name == name);

            return make;
        }

        private async Task<VehicleMake> GetExistingMakeAsync(string name)
        {
            var make = await this.DbContext.VehicleMakes
                                   .FirstOrDefaultAsync(c => c.Name == name);

            return make;
        }

        public async Task<TModel> GetViewMakeAsync<TModel>(int id)
        {
            var make = await this.GetMakeAsync(id);

            if (make == null)
            {
                throw new MyCarAppException(string.Format(Messages.MakeWithIdNoExists, id));
            }

            var model = this.Mapper.Map<TModel>(make);

            return model;
        }

        public async Task<int> AddMakeAsync(VehicleMakeCreationBindingModel model)
        {
            var make = await this.GetExistingMakeAsync(model.Name);

            if (make != null)
            {
                if(make.IsDeleted==false)
                {
                    throw new MyCarAppException(string.Format(Messages.MakeAlreadyExists, model.Name));
                }
            }
            else
            {
                make = this.Mapper.Map<VehicleMake>(model);
            }

            if (model.VehicleModels.Count()>0 && model.VehicleModels.GroupBy(x => x.Name).Any(s => s.Count() > 1))
            {
                throw new MyCarAppException(string.Format(Messages.ModelNameDublicatesExists));
            }

            foreach (var vm in model.VehicleModels)
            {
                if (vm == null || vm.Name == null)
                {
                    throw new MyCarAppException("One or more entity is null!");
                }

                var vModel = this.Mapper.Map<VehicleModel>(vm);
                make.Models.Add(vModel);
            }

            if (make.IsDeleted == true)
            {
                make.IsDeleted = false;
                this.DbContext.Update(make);
            }
            else
            {
                await this.DbContext.VehicleMakes.AddAsync(make);
            }
            await this.DbContext.SaveChangesAsync();

            return make.Id;
        }

        public async Task DeleteMakeAsync(int id)
        {
            var make = await this.GetMakeAsync(id);

            if (make == null)
            {
                throw new MyCarAppException(string.Format(Messages.MakeWithIdNoExists, id));
            }

            make.IsDeleted = true;

            foreach(var model in make.Models)
            {
                model.IsDeleted = true;
            }

            this.DbContext.Update(make);
            this.DbContext.SaveChanges();
        }

        public async Task<VehicleModel> GetModelAsync(int id)
        {
            var model = await this.DbContext.VehicleModels
                .Include(m => m.Make)
                .Where(m => m.IsDeleted == false && m.Make.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);

            return model;
        }

        public async Task<int> AddMakeModelAsync(VehicleModelCreatingBindingModel model)
        {
            var make = await this.GetMakeAsync(model.MakeId);

            if (make == null)
            {
                throw new MyCarAppException(string.Format(Messages.MakeWithIdNoExists, model.Name));
            }

            if(make.Models.Select(m=>m.Name).Any(name=>name==model.Name))
            {
                throw new MyCarAppException(string.Format(Messages.ModelAlreadyExists, model.Name,make.Name));
            }

            var vehicleModel = new VehicleModel()
            {
                MakeId = make.Id,
                Name = model.Name
            };


            await this.DbContext.VehicleModels.AddAsync(vehicleModel);
            await this.DbContext.SaveChangesAsync();

            return make.Id;
        }

        public async Task<int> EditModelAsync(VehicleModelEdittingBindingModel model)
        {
            var vehicleModel = await this.GetModelAsync(model.Id);

            if (vehicleModel == null)
            {
                throw new MyCarAppException(string.Format(Messages.ModelWithIdNoExists, model.Id));
            }

            vehicleModel.Name = model.Name;

            this.DbContext.Update(vehicleModel);
            this.DbContext.SaveChanges();

            return vehicleModel.MakeId;
        }

        public async Task<TModel> GetViewModelAsync<TModel>(int id)
        {
            var vehicleModel = await this.GetModelAsync(id);

            if (vehicleModel == null)
            {
                throw new MyCarAppException(string.Format(Messages.ModelWithIdNoExists, id));
            }

            var viewModel = this.Mapper.Map<TModel>(vehicleModel);

            return viewModel;
        }

        public async Task<int> DeleteModelAsync(int id)
        {
            var vehicleModel = await this.GetModelAsync(id);

            if (vehicleModel == null)
            {
                throw new MyCarAppException(string.Format(Messages.ModelWithIdNoExists, id));
            }

            var makeId = vehicleModel.MakeId;
            vehicleModel.IsDeleted = true;

            this.DbContext.Update(vehicleModel);
            this.DbContext.SaveChanges();

            return makeId;
        }
    }
}
