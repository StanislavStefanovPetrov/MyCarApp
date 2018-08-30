using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.Publisher.ViewModels
{
    public class UserAdvertisementDetailViewModel
    {
        public UserAdvertisementDetailViewModel()
        {
            this.Watchers = new List<UserAdvertisementWatcher>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string SellerId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        [Display(Name = "Is Expired?")]
        public bool IsExpired { get; set; }

        public UserAdvertisementVehicleViewModel Vehicle { get; set; }
        
        public ICollection<UserAdvertisementWatcher> Watchers { get; set; }
    }
}
