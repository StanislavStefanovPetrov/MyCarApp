using Microsoft.AspNetCore.Http;
using MyCarApp.Common.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.Publisher.BindingModels
{
    public class VehicleBindingModel
    {
        [Required]
        [Display(Name = "Type")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Choose a type from available")]
        public int TypeId { get; set; }

        [Required]
        [Display(Name = "Condition")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Choose a condition from available")]
        public int ConditionIds { get; set; }

        [Required]
        [Display(Name = "Make")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Choose a make from available")]
        public int MakeId { get; set; }

        [Required]
        [Display(Name = "Model")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Choose a model from available")]
        public int ModelId { get; set; }

        [Required]
        [Display(Name = "Color")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Choose a color from available")]
        public int ColorId { get; set; }

        [Required]
        [Display(Name = "Transmission")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Transmission should be between 1 and 2147483647")]
        public int TransmissionId { get; set; }

        [Required]
        [Display(Name = "Fuel")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Fuel should be between 1 and 2147483647")]
        public int FuelTypeId { get; set; }

        [Required]
        [Display(Name = "Engine Capacity")]
        [Range(100, 10000, ErrorMessage = "Engine Capacity should be between 100 and 10000")]
        public int EngineCapacity { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CurrentDate]
        [Display(Name ="First Registration")]
        public DateTime FirstRegistration { get; set; }

        [Required]
        [Range(1, 1000000, ErrorMessage = "Kilometre should be between 1 and 1000000")]
        public int Kilometre { get; set; }

        [Required]
        [Range(0.01, Double.PositiveInfinity, ErrorMessage = "Price should be positive")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 2000, ErrorMessage = "Power should be between 1 and 2000")]
        public int Power { get; set; }

        [Required]
        public IFormFile PictureFile { get; set; }
    }
}
