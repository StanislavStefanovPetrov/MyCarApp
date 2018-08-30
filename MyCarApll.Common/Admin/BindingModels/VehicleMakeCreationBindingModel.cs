using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.Admin.BindingModels
{
    public class VehicleMakeCreationBindingModel
    {
        public VehicleMakeCreationBindingModel()
        {
            this.VehicleModels = new List<VehicleModelCreatingBindingModel>();
        }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        [Display(Name = "Make Name")]
        public string Name { get; set; }

        public ICollection<VehicleModelCreatingBindingModel> VehicleModels { get; set; }
    }
}
