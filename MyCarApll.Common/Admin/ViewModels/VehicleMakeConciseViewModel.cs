using System.Collections.Generic;

namespace MyCarApp.Common.Admin.ViewModels
{
    public class VehicleMakeConciseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ModelsCountRegistered { get; set; }

        public ICollection<VehicleModelDetailsViewModel> Models { get; set; }
    }
}
