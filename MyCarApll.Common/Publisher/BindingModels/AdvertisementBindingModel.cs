using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.Publisher.BindingModels
{
    public class AdvertisementBindingModel
    {
        [Required]
        [Display(Name = "Advertisement Title")]
        [MinLength(3), MaxLength(50)]
        public string Title { get; set; }

        [BindProperty(SupportsGet = false)]
        public string SellerId { get; set; }

        [BindProperty(SupportsGet = false)]
        public string SellerUserName { get; set; }

        [BindProperty(SupportsGet = false)]
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }
    }
}
