using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.Admin.BindingModels
{
    public class VehicleModelDeletingBindingModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        [Display(Name = "Model Name")]
        public string Name { get; set; }

        [Required]
        public int MakeId { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        [Display(Name = "Make Name")]
        public string MakeName { get; set; }
    }
}
