using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Models.Vehicles
{
    public class VehicleType
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
