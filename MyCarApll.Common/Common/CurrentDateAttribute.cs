using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MyCarApp.Common.Common
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CurrentDateAttribute : ValidationAttribute,IClientModelValidator
    {
        public const string DateOutOrRange = "The date should be between {0} and {1}";

        public const string NullError = "Please fill the date";
        private const int Year = 1900;
        private const int Month = 1;
        private const int Day = 1;

        private readonly DateTime minDate;
        private readonly DateTime today;

        public CurrentDateAttribute()
        {
            this.minDate = new DateTime(Year, Month, Day);
            this.today = DateTime.Now;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-currentdate", string.Format("Date must be between {0} and {1}", this.minDate.ToString("o"), this.today.ToString("o")));
            context.Attributes.Add("data-val-currentdate-min", string.Format("Date must be after {0}", this.minDate.ToString("o")));
            context.Attributes.Add("data-val-currentdate-max", string.Format("Date must be before {0}", this.today.ToString("o")));
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var dt = (DateTime)value;

                if (dt <= this.today && dt >= this.minDate)
                {
                    return true;
                }
            }

            return false;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var dt = (DateTime)value;

                if (dt <= this.today && dt >= this.minDate)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(string.Format(DateOutOrRange, this.minDate.ToShortDateString(), today.ToShortDateString()));
                }
            }

            return new ValidationResult(NullError);
        }
    }
}
