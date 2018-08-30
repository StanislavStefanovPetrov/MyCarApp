using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyCarApp.Data;
using MyCarApp.Models.Vehicles;
using MyCarApp.Services.Exceptions;
using MyCarApp.Services.Publisher.Interfaces;
using MyCarApp.Services.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyCarApp.Services.Publisher
{
    public class PublisherPictureService : BaseEFService, IPictureService
    {
        private const int maxPictureSize = 5242880;

        public PublisherPictureService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<PicturePath> GetNewPicturePath(IFormFile picture, string userId, string username, int vehicleId)
        {
            var picSize = picture.Length;

            if (picSize > maxPictureSize)
            {
                throw new MyCarAppException(Messages.BigPictureSize);
            }

            var picFileName = picture.FileName;
            var extIndex = picFileName.LastIndexOf('.');
            var picExtention = picFileName.Substring(extIndex);

            var extention = this.DbContext.PictureValidExtensions.FirstOrDefault(e => e.Extension == picExtention);

            if (extention == null)
            {
                throw new MyCarAppException(string.Format(Messages.ForbiddenFileExtention, picExtention));
            }

            var newFileName = string.Format("{0}-{1}-{2}-{3}", username, userId, vehicleId, picFileName);
            var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Pictures", newFileName);

            try
            {
                using (var fileStream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await picture.CopyToAsync(fileStream);
                }

                var picturePath = this.DbContext.PicturePaths.FirstOrDefault(e => e.Path == newFileName);

                if (picturePath == null)
                {
                    picturePath = new PicturePath()
                    {
                        Extension = extention,
                        VehicleId = vehicleId,
                        Path = newFileName
                    };

                    this.DbContext.PicturePaths.Add(picturePath);
                }

                return picturePath;
            }
            catch (Exception ex)
            {
                throw new MyCarAppException(ex.Message);
            }
        }
    }
}
