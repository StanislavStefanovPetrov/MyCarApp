using Microsoft.AspNetCore.Identity;
using MyCarApp.Models.Advertisements;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Models
{
    public class ApplicationUser: IdentityUser
    {
        public ApplicationUser()
        {
            this.Advertisements = new List<Advertisement>();
            this.TrackAdvertisements = new List<AdvertisementUser>();
        }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string FullName { get; set; }

        public ICollection<Advertisement> Advertisements { get; set; }

        public ICollection<AdvertisementUser> TrackAdvertisements { get; set; }
    }
}
