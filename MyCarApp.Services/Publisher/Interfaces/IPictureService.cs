using Microsoft.AspNetCore.Http;
using MyCarApp.Models.Vehicles;
using System.Threading.Tasks;

namespace MyCarApp.Services.Publisher.Interfaces
{
    public interface IPictureService
    {
        Task<PicturePath> GetNewPicturePath(IFormFile picture, string userId, string username, int vehicleId);
    }
}
