using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.User.ViewModels
{
    public class DetailAdvertisementSellerViewModel
    {
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }
    }
}
