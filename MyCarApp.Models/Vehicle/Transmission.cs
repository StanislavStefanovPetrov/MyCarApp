using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Models.Vehicles
{
    public class Transmission
    {
        public Transmission()
        {
            this.Vehicles = new List<Vehicle>();
        }

        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
