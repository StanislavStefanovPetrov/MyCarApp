using System;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.User.ViewModels
{
    public class DetailAdvertisementViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Is Expired?")]
        public bool IsExpired { get; set; }

        [Display(Name = "Seller Info")]
        public DetailAdvertisementSellerViewModel Seller { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        [Display(Name = "Vehicle Info")]
        public DetailAdvertisementVehicleViewModel Vehicle { get; set; }
    }
}
