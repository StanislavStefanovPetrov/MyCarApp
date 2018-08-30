using MyCarApp.Common.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Models.Vehicles
{
    public class Vehicle
    {
        public Vehicle()
        {
            this.PicturePaths = new List<PicturePath>();
        }

        public int Id { get; set; }

        public VehicleType VehicleType { get; set; }

        public VehicleCondition Condition { get; set; }

        public VehicleMake Make { get; set; }

        public VehicleModel Model { get; set; }

        [DataType(DataType.Date)]
        [CurrentDate]
        public DateTime FirstRegistration { get; set; }

        [Range(1, 1000000, ErrorMessage = "Kilometre should be between 1 and 1000000")]
        public int Kilometre { get; set; }

        [Range(0.01, Double.PositiveInfinity, ErrorMessage = "Price should be positive")]
        public decimal Price { get; set; }

        [Range(1, 2000, ErrorMessage = "Power should be between 1 and 2000")]
        public int Power { get; set; }

        public Engine Engine { get; set; }

        public FuelType FuelType { get; set; }

        public Colour VehicleExteriorColour { get; set; }

        public ICollection<PicturePath> PicturePaths { get; set; }

        public Transmission Transmission { get; set; }
    }
}
