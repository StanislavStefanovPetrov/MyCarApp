using MyCarApp.Common.Admin.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.Admin.BindingModels
{
    public class VehicleMakeDeletingBindingModel
    {
        public VehicleMakeDeletingBindingModel()
        {
            this.VehicleModels = new List<VehicleModelMakeDeleteViewModel>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        [Display(Name = "Make Name")]
        public string Name { get; set; }

        public ICollection<VehicleModelMakeDeleteViewModel> VehicleModels { get; set; }
    }
}
