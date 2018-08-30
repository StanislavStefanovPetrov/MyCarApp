using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Models.Vehicles
{
    public class Colour
    {
        public Colour()
        {
            this.Vehicles = new List<Vehicle>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
