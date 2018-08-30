using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCarApp.Common.Admin.ViewModels;
using MyCarApp.Common.Utilities;
using MyCarApp.Data;
using MyCarApp.Models;
using MyCarApp.Tests.Mocks;
using MyCarApp.Web.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyCarApp.Tests
{
    [TestClass]
    public class AdminUsersControllerTests
    {
        private ApplicationDbContext dbContext;
        private IMapper mapper;

        [TestMethod]
        public void UserController_ShoutBeAcceptableByAdmin()
        {
            //Arrange
            var userController = new UsersController(this.dbContext, this.mapper, null)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                       {new Claim(ClaimTypes.Role, AppUtilities.AdministratorRole)}))
                    }
                }
            };

            //Act
            var result = userController.User.IsInRole(AppUtilities.AdministratorRole);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Index_ReturnsAllUsersExceptCurrent()
        {
            //Arrange
            ClaimsPrincipal currentUser = SetCurrentUserAsAdmin();

            ApplicationUser[] appUsers = await AddUsersInDbContext();

            Mock<UserManager<ApplicationUser>> mockUserManager = CreateMockUserManager();

            mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(appUsers[1]);
            mockUserManager.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<string>());

            UsersController userController = CreateController(currentUser, mockUserManager);

            //Act
            var result = await userController.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            var models = result.Model as IEnumerable<UserConciseViewModel>;
            CollectionAssert.AreEqual(new[] { "111", "333", "444" }, models.Select(u => u.Id).ToArray());
        }       

        [TestMethod]
        public async Task Details_ReturnsCorrectUser()
        {
            //Arrange
            ClaimsPrincipal currentUser = SetCurrentUserAsAdmin();

            ApplicationUser[] appUsers = await AddUsersInDbContext();

            Mock<UserManager<ApplicationUser>> mockUserManager = CreateMockUserManager();

            mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(appUsers[1]);

            mockUserManager.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<string>());

            UsersController userController = CreateController(currentUser, mockUserManager);

            //Act
            var result = await userController.Details("111") as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            var models = result.Model as UserDetailsViewModel;
            Assert.AreEqual("111", models.Id);
        }

        [TestMethod]
        public async Task Details_ReturnsUnauthorizedResult_WhenUserIdIsSame()
        {
            //Arrange
            ClaimsPrincipal currentUser = SetCurrentUserAsAdmin();

            ApplicationUser[] appUsers = await AddUsersInDbContext();

            Mock<UserManager<ApplicationUser>> mockUserManager = CreateMockUserManager();

            mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(appUsers[1]);

            UsersController userController = CreateController(currentUser, mockUserManager);

            //Act
            var result = await userController.Details("222") as UnauthorizedResult;
            //Assert
            Assert.IsNotNull(result);
            var statusCode = result.StatusCode;
            Assert.AreEqual(401, statusCode);
        }

        [TestMethod]
        public async Task Details_ReturnsNotFoundResult_WhenUserIdDoesNotExists()
        {
            //Arrange
            ClaimsPrincipal currentUser = SetCurrentUserAsAdmin();

            ApplicationUser[] appUsers = await AddUsersInDbContext();

            Mock<UserManager<ApplicationUser>> mockUserManager = CreateMockUserManager();

            mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(appUsers[1]);

            UsersController userController = CreateController(currentUser, mockUserManager);

            //Act
            var result = await userController.Details("555") as NotFoundResult;
            //Assert
            Assert.IsNotNull(result);
            var statusCode = result.StatusCode;
            Assert.AreEqual(404, statusCode);
        }

        [TestMethod]
        public async Task Details_ReturnsUnauthorizedResult_WhenUserIsAdmin()
        {
            //Arrange
            ClaimsPrincipal currentUser = SetCurrentUserAsAdmin();

            ApplicationUser[] appUsers = await AddUsersInDbContext();

            Mock<UserManager<ApplicationUser>> mockUserManager = CreateMockUserManager();

            mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(appUsers[1]);

            mockUserManager.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<string>() { AppUtilities.AdministratorRole});

            UsersController userController = CreateController(currentUser, mockUserManager);

            //Act
            var result = await userController.Details("111") as UnauthorizedResult;
            //Assert
            Assert.IsNotNull(result);
            var statusCode = result.StatusCode;
            Assert.AreEqual(401, statusCode);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.dbContext = MockDbContext.GetContext();
            this.mapper = MockAutoMapper.Instance;
        }
        
        private static Mock<UserManager<ApplicationUser>> CreateMockUserManager()
        {
            var mockStore = new Mock<IUserStore<ApplicationUser>>();

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            return mockUserManager;
        }

        private async Task<ApplicationUser[]> AddUsersInDbContext()
        {
            var appUsers = new[]
                                    {
                new ApplicationUser(){Id="111",FullName="first"},
                new ApplicationUser(){Id="222",FullName="second"},
                new ApplicationUser(){Id="333",FullName="third"},
                new ApplicationUser(){Id="444",FullName="fourth"}
            };


            await this.dbContext.Users.AddRangeAsync(appUsers);
            await this.dbContext.SaveChangesAsync();
            return appUsers;
        }

        private static ClaimsPrincipal SetCurrentUserAsAdmin()
        {
            return new ClaimsPrincipal(new ClaimsIdentity(new[]
                                   {new Claim(ClaimTypes.Role, AppUtilities.AdministratorRole)}));
        }

        private UsersController CreateController(ClaimsPrincipal currentUser, Mock<UserManager<ApplicationUser>> mockUserManager)
        {
            return new UsersController(this.dbContext, this.mapper, mockUserManager.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = currentUser
                    }
                }
            };
        }
    }
}
