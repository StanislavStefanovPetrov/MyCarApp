using Microsoft.AspNetCore.Mvc;
using MyCarApp.Services.Publisher.Interfaces;
using System.Threading.Tasks;

namespace MyCarApp.Web.Controllers
{
    public class ModelsController : Controller
    {
        private readonly IPublisherVehicleService vehicleService;

        public ModelsController(IPublisherVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int makeId)
        {
            var models = await this.vehicleService.GetModelsAsync(makeId);

            return PartialView("_Index",models);
        }
    }
}