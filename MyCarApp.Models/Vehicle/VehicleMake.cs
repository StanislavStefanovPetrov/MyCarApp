using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Models.Vehicles
{
    public class VehicleMake
    {
        public VehicleMake()
        {
            this.Models = new List<VehicleModel>();
            this.Vehicles = new List<Vehicle>();
            this.IsDeleted = false;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<VehicleModel> Models { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
