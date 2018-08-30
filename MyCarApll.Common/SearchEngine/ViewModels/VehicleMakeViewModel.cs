using System.Collections.Generic;

namespace MyCarApp.Common.SearchEngine.ViewModels
{
    public class VehicleMakeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<VehicleModelViewModel> Models { get; set; }
    }
}