using MyCarApp.Common.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.SearchEngine.BindingModels
{
    public class SearchEngineVehicleBindingModel
    {
        public SearchEngineVehicleBindingModel()
        {
            this.FirstRegistrationAfter = new DateTime(1900, 1, 1);
            this.FirstRegistrationBefore = DateTime.Now;
            this.MinPrice = 0.00m;
            this.MaxPrice = decimal.MaxValue;
            this.MaxEngineCapacity = 10000;
            this.MaxKilometre = 1000000;
            this.MaxPower = 2000;
            this.MaxPrice = decimal.MaxValue;
        }
        [Required]
        [Display(Name = "Make")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Choose a make from available")]
        public int MakeId { get; set; }

        [Required]
        [Display(Name = "Model")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Choose a model from available")]
        public int ModelId { get; set; }

        public ICollection<ColorBindingModel> Colors { get; set; }

        public ICollection<FuelTypeBindingModel> FuelTypes { get; set; }

        public ICollection<TransmissionBindingModel> Transmissions { get; set; }

        public ICollection<VehicleConditionBindingModel> VehicleConditions { get; set; }

        public ICollection<VehicleTypeBindingModel> VehicleTypes { get; set; }

        [Required]
        [Display(Name = "Min Engine Capacity")]
        [Range(0, 10000, ErrorMessage = "Engine Capacity should be between 0 and 10000")]
        public int MinEngineCapacity { get; set; }

        [Required]
        [Display(Name = "Max Engine Capacity")]
        [Range(0, 10000, ErrorMessage = "Engine Capacity should be between 0 and 10000")]
        public int MaxEngineCapacity { get; set; }

        [DataType(DataType.Date)]
        [CurrentDate]
        [Display(Name = "First Registration After")]
        [DateLessThan("FirstRegistrationBefore", ErrorMessage = "\"First Registration After\" must be before \"First Registration Before\"")]
        public DateTime FirstRegistrationAfter { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CurrentDate]
        [Display(Name = "First Registration Before")]
        public DateTime FirstRegistrationBefore { get; set; }

        [Required]
        [Range(0, 1000000, ErrorMessage = "Kilometre should be between 0 and 1000000")]
        [Display(Name = "Min Kilometre")]
        [ValueLessThanInt("MaxKilometre", ErrorMessage = "\"Min Kilometre\" must be less than \"Max Kilometre\"")]
        public int MinKilometre { get; set; }

        [Required]
        [Range(0, 1000000, ErrorMessage = "Kilometre should be between 0 and 1000000")]
        [Display(Name = "Max Kilometre")]
        public int MaxKilometre { get; set; }

        [Required]
        [Range(0.00, Double.PositiveInfinity, ErrorMessage = "Price should be positive")]
        [Display(Name = "Min Price")]
        [ValueLessThanDecimal("MaxPrice", ErrorMessage = "\"Min Price\" must be less than \"Max Price\"")]
        public decimal MinPrice { get; set; }

        [Required]
        [Range(0.00, Double.PositiveInfinity, ErrorMessage = "Price should be positive")]
        [Display(Name = "Max Price")]
        public decimal MaxPrice { get; set; }

        [Required]
        [Range(0, 2000, ErrorMessage = "Power should be between 0 and 2000")]
        [Display(Name = "Min Power")]
        [ValueLessThanInt("MaxPower", ErrorMessage = "\"Min Power\" must be less than \"Max Power\"")]
        public int MinPower { get; set; }

        [Required]
        [Range(0, 2000, ErrorMessage = "Power should be between 0 and 2000")]
        [Display(Name = "Max Power")]
        public int MaxPower { get; set; }
    }
}
