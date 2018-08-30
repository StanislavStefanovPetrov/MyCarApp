using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Models.Vehicles
{
    public class PicturePath
    {
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public int ExtensionId { get; set; }
        public PictureValidExtensions Extension { get; set; }
    }
}
