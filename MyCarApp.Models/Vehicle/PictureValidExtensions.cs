using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Models.Vehicles
{
    public class PictureValidExtensions
    {
        public int Id { get; set; }

        [Required]
        public string Extension { get; set; }
    }
}
