using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Models.Vehicles
{
    public class VehicleModel
    {
        public VehicleModel()
        {
            this.IsDeleted = false;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Name { get; set; }

        public int MakeId { get; set; }

        public VehicleMake Make { get; set; }

        public bool IsDeleted { get; set; }
    }
}
