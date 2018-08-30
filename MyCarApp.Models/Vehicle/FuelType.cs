using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Models.Vehicles
{
    public class FuelType
    {
        public FuelType()
        {
            this.Engines = new List<EngineFuelType>();
            this.Vehicles = new List<Vehicle>();
        }

        public int Id { get; set; }

        [Required]
        public string Fuel { get; set; }

        public ICollection<EngineFuelType> Engines { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
