using System;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.User.ViewModels
{
    public class IndexAdvertisementAllViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Is Expired")]
        public bool IsExpired { get; set; }

        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        public string Vehicle { get; set; }

        public string Image { get; set; }
    }
}
