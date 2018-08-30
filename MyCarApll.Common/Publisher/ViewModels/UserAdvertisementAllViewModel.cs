using System;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.Publisher.ViewModels
{
    public class UserAdvertisementAllViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        public string Vehicle { get; set; }

        [Display(Name ="Is Expired?")]
        public bool IsExpired { get; set; }

        public int Watchers { get; set; }
    }
}
