using AutoMapper;
using MyCarApp.Common.Admin.BindingModels;
using MyCarApp.Common.Admin.ViewModels;
using MyCarApp.Common.Publisher.ViewModels;
using MyCarApp.Common.SearchEngine.BindingModels;
using MyCarApp.Models;
using MyCarApp.Models.Advertisements;
using MyCarApp.Models.Vehicles;
using System.Linq;

namespace MyCarApp.Web.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<VehicleMake, VehicleMakeConciseViewModel>().ForMember(dest => dest.ModelsCountRegistered,
               opts => opts.MapFrom(src => src.Models.Count));
            this.CreateMap<VehicleModel, VehicleModelCreatingBindingModel>();

            this.CreateMap<VehicleMakeCreationBindingModel, VehicleMake>();
            this.CreateMap<VehicleModelCreatingBindingModel, VehicleModel>();

            this.CreateMap<VehicleMake, VehicleMakeDeletingBindingModel>().ForMember(dest => dest.VehicleModels,
               opts => opts.MapFrom(src => src.Models));
            this.CreateMap<VehicleModel, VehicleModelMakeDeleteViewModel>().ForMember(dest => dest.ModelName,
               opts => opts.MapFrom(src => src.Name));

            this.CreateMap<ColorViewModel, Colour>();
            this.CreateMap<EngineViewModel, Engine>();
            this.CreateMap<FuelTypeViewModel, FuelType>();
            this.CreateMap<TransmissionViewModel, Transmission>();
            this.CreateMap<VehicleConditionViewModel, VehicleCondition>();
            this.CreateMap<VehicleMakeViewModel, VehicleMake>();
            this.CreateMap<VehicleModelViewModel, VehicleModel>();
            this.CreateMap<VehicleTypeViewModel, VehicleType>();

            this.CreateMap<VehicleType, VehicleTypeBindingModel>();
            this.CreateMap<Colour, ColorBindingModel>();
            this.CreateMap<FuelType, FuelTypeBindingModel>();
            this.CreateMap<Transmission, TransmissionBindingModel>();
            this.CreateMap<VehicleCondition, VehicleConditionBindingModel>();

            this.CreateMap<ApplicationUser, UserDetailsViewModel>();
            this.CreateMap<ApplicationUser, ChangeUserPasswordBindingModel>();
            this.CreateMap<ApplicationUser, SetUserToBannedBindingModel>();
            this.CreateMap<ApplicationUser, SetUserToUnBannedBindingModel>();
        }
    }
}
