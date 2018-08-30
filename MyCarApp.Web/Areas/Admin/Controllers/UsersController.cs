using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCarApp.Common.Admin.BindingModels;
using MyCarApp.Common.Admin.ViewModels;
using MyCarApp.Common.Utilities;
using MyCarApp.Data;
using MyCarApp.Models;
using MyCarApp.Services.Utilities;
using MyCarApp.Web.Helpers;
using MyCarApp.Web.Helpers.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCarApp.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private const int BanTime = 5;

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(
            ApplicationDbContext context,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var users = this.context.Users
                .Where(u => u.Id != currentUser.Id)
                .ToList();

            var models = new List<UserConciseViewModel>();

            foreach (var user in users)
            {
                var Roles = await this.userManager.GetRolesAsync(user);

                var isLockedOut = await this.userManager.IsLockedOutAsync(user);

                var userViewModel = new UserConciseViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    IsLockedOut=isLockedOut,
                    Roles = await this.userManager.GetRolesAsync(user)
                };

                models.Add(userViewModel);
            }
            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            if (id == currentUser.Id)
            {
                return Unauthorized();
            }

            var user = await this.context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await this.userManager.GetRolesAsync(user);
            if (roles.Any(r => r == AppUtilities.AdministratorRole))
            {
                return Unauthorized();
            }

            var model = this.mapper.Map<UserDetailsViewModel>(user);
            model.Roles = roles;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            if (id == currentUser.Id)
            {
                return Unauthorized();
            }

            var user = await this.context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await this.userManager.GetRolesAsync(user);
            if (roles.Any(r => r == AppUtilities.AdministratorRole))
            {
                return Unauthorized();
            }
            var hasPassword = await this.userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return Unauthorized();
            }

            var model = this.mapper.Map<ChangeUserPasswordBindingModel>(user);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordBindingModel bindingModel)
        {
            if (!this.ModelState.IsValid)
            {
                foreach (var value in this.ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        var msg = new MessageModel()
                        {
                            Type = MessageType.Danger,
                            Message = error.ErrorMessage
                        };

                        this.TempData.Put("__Message", msg);
                    }
                }
                return RedirectToAction("Index", "Users");
            }

            var currentUser = await this.userManager.GetUserAsync(this.User);
            if (bindingModel.Id == currentUser.Id)
            {
                return Unauthorized();
            }

            var dbUser = this.context.Users.SingleOrDefault(u => u.Id == bindingModel.Id && u.Email==bindingModel.Email);
            if (dbUser == null)
            {
                return NotFound();
            }
            var roles = await this.userManager.GetRolesAsync(dbUser);
            if (roles.Any(r => r == AppUtilities.AdministratorRole))
            {
                return Unauthorized();
            }
            var hasPassword = await this.userManager.HasPasswordAsync(dbUser);
            if (!hasPassword)
            {
                return Unauthorized();
            }

            var changePasswordResult = await this.userManager.ChangePasswordAsync(dbUser, bindingModel.OldPassword, bindingModel.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return this.View(bindingModel);
            }

            var msgSuccess = new MessageModel()
            {
                Type = MessageType.Success,
                Message = Messages.PasswordSuccessfullyChanged
            };

            this.TempData.Put("__Message", msgSuccess);

            return RedirectToAction("Index","Users");
        }

        [HttpGet]
        public async Task<IActionResult> Ban(string id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            if (id == currentUser.Id)
            {
                return Unauthorized();
            }

            var user = await this.context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await this.userManager.GetRolesAsync(user);
            if (roles.Any(r => r == AppUtilities.AdministratorRole))
            {
                return Unauthorized();
            }

            var model = this.mapper.Map<SetUserToBannedBindingModel>(user);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Ban(SetUserToBannedBindingModel bindingModel)
        {
            if (!this.ModelState.IsValid)
            {
                foreach (var value in this.ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        var msg = new MessageModel()
                        {
                            Type = MessageType.Danger,
                            Message = error.ErrorMessage
                        };

                        this.TempData.Put("__Message", msg);
                    }
                }
                return RedirectToAction("Index", "Users");
            }

            var currentUser = await this.userManager.GetUserAsync(this.User);
            if (bindingModel.Id == currentUser.Id)
            {
                return Unauthorized();
            }

            var dbUser = this.context.Users.SingleOrDefault(u => u.Id == bindingModel.Id && u.Email == bindingModel.Email);
            if (dbUser == null)
            {
                return NotFound();
            }
            var roles = await this.userManager.GetRolesAsync(dbUser);
            if (roles.Any(r => r == AppUtilities.AdministratorRole))
            {
                return Unauthorized();
            }

            dbUser.LockoutEnabled = true;
            dbUser.LockoutEnd = DateTime.UtcNow.AddMinutes(BanTime);
            await userManager.UpdateAsync(dbUser);

            var msgSuccess = new MessageModel()
            {
                Type = MessageType.Success,
                Message = Messages.UserSuccessfullyBanned
            };

            this.TempData.Put("__Message", msgSuccess);

            return RedirectToAction("Index", "Users");
        }


        [HttpGet]
        public async Task<IActionResult> UnBan(string id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            if (id == currentUser.Id)
            {
                return Unauthorized();
            }

            var user = await this.context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await this.userManager.GetRolesAsync(user);
            if (roles.Any(r => r == AppUtilities.AdministratorRole))
            {
                return Unauthorized();
            }

            var model = this.mapper.Map<SetUserToUnBannedBindingModel>(user);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UnBan(SetUserToUnBannedBindingModel bindingModel)
        {
            if (!this.ModelState.IsValid)
            {
                foreach (var value in this.ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        var msg = new MessageModel()
                        {
                            Type = MessageType.Danger,
                            Message = error.ErrorMessage
                        };

                        this.TempData.Put("__Message", msg);
                    }
                }
                return RedirectToAction("Index", "Users");
            }

            var currentUser = await this.userManager.GetUserAsync(this.User);
            if (bindingModel.Id == currentUser.Id)
            {
                return Unauthorized();
            }

            var dbUser = this.context.Users.SingleOrDefault(u => u.Id == bindingModel.Id && u.Email == bindingModel.Email);
            if (dbUser == null)
            {
                return NotFound();
            }
            var roles = await this.userManager.GetRolesAsync(dbUser);
            if (roles.Any(r => r == AppUtilities.AdministratorRole))
            {
                return Unauthorized();
            }

            dbUser.LockoutEnabled = true;
            dbUser.LockoutEnd = null;
            await userManager.UpdateAsync(dbUser);

            var msgSuccess = new MessageModel()
            {
                Type = MessageType.Success,
                Message = Messages.UserSuccessfullyUnBanned
            };

            this.TempData.Put("__Message", msgSuccess);

            return RedirectToAction("Index", "Users");
        }
    }
}