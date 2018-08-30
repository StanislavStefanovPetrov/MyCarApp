using Microsoft.AspNetCore.Mvc;
using MyCarApp.Services.SearchEngine.Interfaces;
using System.Threading.Tasks;

namespace MyCarApp.Web.Controllers
{
    public class SearchModelsController : Controller
    {
        private readonly ISearchEngineVehicleService vehicleService;

        public SearchModelsController(ISearchEngineVehicleService vehicleService)
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