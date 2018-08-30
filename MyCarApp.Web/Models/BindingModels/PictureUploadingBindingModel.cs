using Microsoft.AspNetCore.Http;

namespace MyCarApp.Web.Models.BindingModels
{
    public class PictureUploadingBindingModel
    {
        public int PublisherId { get; set; }

        public IFormFile PictureFile { get; set; }
    }
}
