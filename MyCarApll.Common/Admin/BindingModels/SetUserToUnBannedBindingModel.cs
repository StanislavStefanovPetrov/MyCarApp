using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.Admin.BindingModels
{
    public class SetUserToUnBannedBindingModel
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
    }
}
