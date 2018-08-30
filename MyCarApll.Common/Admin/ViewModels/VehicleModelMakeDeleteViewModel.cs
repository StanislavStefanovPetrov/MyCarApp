using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.Admin.ViewModels
{
    public class VehicleModelMakeDeleteViewModel
    {
        [Display(Name = "Model Name")]
        public string ModelName { get; set; }
    }
}
