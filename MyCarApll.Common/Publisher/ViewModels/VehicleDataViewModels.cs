using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.Publisher.ViewModels
{
    public class VehicleDataViewModels
    {
        public ICollection<ColorViewModel> Colors { get; set; }

        public ICollection<EngineViewModel> Engines { get; set; }

        public ICollection<FuelTypeViewModel> FuelTypes { get; set; }

        public ICollection<TransmissionViewModel> Transmissions { get; set; }

        public ICollection<VehicleConditionViewModel> VehicleConditions { get; set; }

        public ICollection<VehicleMakeViewModel> VehicleMakes { get; set; }

        public ICollection<VehicleTypeViewModel> VehicleTypes { get; set; }
    }
}
