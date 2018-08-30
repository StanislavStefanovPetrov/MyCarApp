using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyCarApp.Common.SearchEngine.BindingModels
{
    public class ColorBindingModel
    {
        [Required]
        [BindProperty(SupportsGet = false)]
        public int Id { get; set; }

        [Required]
        [BindProperty(SupportsGet = false)]
        public string Name { get; set; }

        [BindProperty]
        public bool IsChecked { get; set; }
    }
}