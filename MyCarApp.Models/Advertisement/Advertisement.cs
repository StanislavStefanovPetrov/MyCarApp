using MyCarApp.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Models.Advertisements
{
    public class Advertisement
    {
        public Advertisement()
        {
            this.IsExpired = false;
            this.Watchers = new List<AdvertisementUser>();
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Advertisement Title")]
        [MinLength(3), MaxLength(50)]
        public string Title { get; set; }

        public string SellerId { get; set; }
        public ApplicationUser Seller { get; set; }

        public DateTime PublishDate { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public bool IsExpired { get; set; }

        public ICollection<AdvertisementUser> Watchers { get; set; }

        public void SetExpiredAdvertisement()
        {
            this.IsExpired = true;
        }
    }
}
