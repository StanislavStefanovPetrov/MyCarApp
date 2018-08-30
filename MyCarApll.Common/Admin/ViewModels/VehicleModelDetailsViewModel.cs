using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.Admin.ViewModels
{
    public class VehicleModelDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Model Name")]
        public string Name { get; set; }

        public int MakeId { get; set; }

        [Display(Name = "Make Name")]
        public string MakeName { get; set; }
    }
}
