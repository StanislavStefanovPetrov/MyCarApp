using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCarApp.Models.Vehicles
{
    public class VehicleCondition
    {
        public VehicleCondition()
        {
            this.Vehicles = new List<Vehicle>();
        }
        public int Id { get; set; }

        [Required]
        public string Condition { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
