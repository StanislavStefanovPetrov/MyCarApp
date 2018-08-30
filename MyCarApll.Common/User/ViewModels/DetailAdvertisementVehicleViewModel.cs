using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.User.ViewModels
{
    public class DetailAdvertisementVehicleViewModel
    {
        public DetailAdvertisementVehicleViewModel()
        {
            this.Images = new List<string>();
        }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Condition")]
        public string Condition { get; set; }

        [Display(Name = "Make")]
        public string Make { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "First Registration")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FirstRegistration { get; set; }

        public int Kilometre { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }

        public int Power { get; set; }

        [Display(Name = "Engine Cubic Capacity")]
        public int EngineCubicCapacity { get; set; }

        [Display(Name = "Fuel Type")]
        public string FuelType { get; set; }

        [Display(Name = "Vehicle Exterior Colour")]
        public string VehicleExteriorColour { get; set; }

        public IList<string> Images { get; set; }

        [Display(Name = "Transmission")]
        public string Transmission { get; set; }
    }
}
