using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCarApp.Common.Admin.BindingModels;
using MyCarApp.Common.Admin.ViewModels;
using MyCarApp.Services.Admin.Interfaces;
using MyCarApp.Services.Exceptions;
using MyCarApp.Web.Helpers;
using MyCarApp.Web.Helpers.Messages;

namespace MyCarApp.Web.Areas.Admin.Controllers
{
    public class VehiclesController : AdminController
    {
        private readonly IAdminVehicleService vehicleService;

        public VehiclesController(
            IAdminVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var makes = await this.vehicleService.GetMakesAsync();
            return View(makes);
        }

        [HttpGet]
        public IActionResult CreateMake()
        {
            VehicleMakeCreationBindingModel model = new VehicleMakeCreationBindingModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMake(VehicleMakeCreationBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                int id = await this.vehicleService.AddMakeAsync(model);

                var msg = new MessageModel()
                {
                    Type = MessageType.Success,
                    Message = "Make created successfully."
                };

                this.TempData.Put("__Message", msg);

                return RedirectToAction("MakeDetails", new { id });
            }
            catch(MyCarAppException ex)
            {
                var msg = new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = ex.Message
                };

                this.TempData.Put("__Message", msg);

                return RedirectToAction("CreateMake");
            }
        }

        [HttpGet]
        public async Task<IActionResult> MakeDetails(int id)
        {
            try
            {
                var model = await this.vehicleService.GetViewMakeAsync< VehicleMakeConciseViewModel>(id);

                return View(model);
            }
            catch (MyCarAppException ex)
            {
                var msg = new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = ex.Message
                };

                this.TempData.Put("__Message", msg);

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteMake(int id)
        {
            try
            {
                var model = await this.vehicleService.GetViewMakeAsync<VehicleMakeDeletingBindingModel>(id);

                return View(model);
            }
            catch (MyCarAppException ex)
            {
                var msg = new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = ex.Message
                };

                this.TempData.Put("__Message", msg);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMake(VehicleMakeDeletingBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await this.vehicleService.DeleteMakeAsync(model.Id);

                var msg = new MessageModel()
                {
                    Type = MessageType.Success,
                    Message = "Make deleted successfully."
                };

                this.TempData.Put("__Message", msg);
                
            }
            catch (MyCarAppException ex)
            {
                var msg = new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = ex.Message
                };

                this.TempData.Put("__Message", msg);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ModelDetails(int id)
        {
            try
            {
                var model = await this.vehicleService.GetViewModelAsync< VehicleModelDetailsViewModel>(id);

                return View(model);
            }
            catch (MyCarAppException ex)
            {
                var msg = new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = ex.Message
                };

                this.TempData.Put("__Message", msg);

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddNewModel(int id)
        {
            try
            {
                var make = await this.vehicleService.GetViewMakeAsync<VehicleMakeConciseViewModel>(id);

                var model = new VehicleModelCreatingBindingModel()
                {
                    MakeId = make.Id,
                    MakeName=make.Name
                };

                return View(model);
            }
            catch (MyCarAppException ex)
            {
                var msg = new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = ex.Message
                };

                this.TempData.Put("__Message", msg);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewModel(VehicleModelCreatingBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                int id = await this.vehicleService.AddMakeModelAsync(model);

                var msg = new MessageModel()
                {
                    Type = MessageType.Success,
                    Message = "Model created successfully."
                };

                this.TempData.Put("__Message", msg);

                return RedirectToAction("MakeDetails", new { id });
            }
            catch (MyCarAppException ex)
            {
                var msg = new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = ex.Message
                };

                this.TempData.Put("__Message", msg);

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditModel(int id)
        {
            try
            {
                var model = await this.vehicleService.GetViewModelAsync<VehicleModelEdittingBindingModel>(id);

                return View(model);
            }
            catch (MyCarAppException ex)
            {
                var msg = new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = ex.Message
                };

                this.TempData.Put("__Message", msg);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditModel(VehicleModelEdittingBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                int id = await this.vehicleService.EditModelAsync(model);

                var msg = new MessageModel()
                {
                    Type = MessageType.Success,
                    Message = "Model edited successfully."
                };

                this.TempData.Put("__Message", msg);

                return RedirectToAction("MakeDetails", new { id });
            }
            catch (MyCarAppException ex)
            {
                var msg = new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = ex.Message
                };

                this.TempData.Put("__Message", msg);

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteModel(int id)
        {
            try
            {
                var model = await this.vehicleService.GetViewModelAsync<VehicleModelDeletingBindingModel>(id);

                return View(model);
            }
            catch (MyCarAppException ex)
            {
                var msg = new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = ex.Message
                };

                this.TempData.Put("__Message", msg);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteModel(VehicleModelDeletingBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                int id = await this.vehicleService.DeleteModelAsync(model.Id);

                var msg = new MessageModel()
                {
                    Type = MessageType.Success,
                    Message = "Model deleted successfully."
                };

                this.TempData.Put("__Message", msg);

                return RedirectToAction("MakeDetails", new { id });
            }
            catch (MyCarAppException ex)
            {
                var msg = new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = ex.Message
                };

                this.TempData.Put("__Message", msg);

                return RedirectToAction("Index");
            }
        }
    }
}