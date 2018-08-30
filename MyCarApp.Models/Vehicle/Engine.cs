using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Models.Vehicles
{
    public class Engine
    {
        public Engine()
        {
            this.Vehicles = new List<Vehicle>();
            this.FuelTypes = new List<EngineFuelType>();
        }

        public int Id { get; set; }

        [Range(100, 10000, ErrorMessage = "Engine Capacity should be between 100 and 10000")]
        public int CubicCapacity { get; set; }

        public ICollection<EngineFuelType> FuelTypes { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
