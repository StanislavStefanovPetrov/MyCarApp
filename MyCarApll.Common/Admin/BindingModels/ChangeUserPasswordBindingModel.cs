using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.Admin.BindingModels
{
    public class ChangeUserPasswordBindingModel
    {
        [Required]
        [Display(Name = "Current Id")]
        [MinLength(10)]
        [MaxLength(64)]
        [BindProperty]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Username")]
        [BindProperty]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        [BindProperty]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [BindProperty]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        [BindProperty]
        public string ConfirmPassword { get; set; }
    }
}
