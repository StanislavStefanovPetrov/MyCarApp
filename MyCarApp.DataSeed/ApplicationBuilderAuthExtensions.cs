using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyCarApp.Common.Utilities;
using MyCarApp.Models;
using System.Threading.Tasks;

namespace MyCarApp.DataSeed
{
    public static class ApplicationBuilderAuthExtensions
    {
        private static readonly IdentityRole[] roles =
        {
            new IdentityRole(AppUtilities.AdministratorRole),
            new IdentityRole(AppUtilities.PublisherRole),
             new IdentityRole(AppUtilities.ObserverRole)
        };

        public static async void SeedDatabase(this IApplicationBuilder app)
        {
            var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = serviceFactory.CreateScope();
            using (scope)
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(role);
                    }
                }

                var user = await userManager.FindByEmailAsync(AppUtilities.AdminIdentification);

                user = await CreatingUser(userManager, user, AppUtilities.AdminIdentification);

                user = await userManager.FindByEmailAsync(AppUtilities.SecondAdminIdentification);

                user = await CreatingUser(userManager, user, AppUtilities.SecondAdminIdentification);
            }
        }

        private static async Task<ApplicationUser> CreatingUser(UserManager<ApplicationUser> userManager, ApplicationUser user, string email)
        {
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = email,
                    Email = email,
                    FullName= AppUtilities.DefaultAdminFullName
                };

                await userManager.CreateAsync(user, AppUtilities.DefaultAdminPassword);
                await userManager.AddToRoleAsync(user, roles[0].Name);
            }

            return user;
        }
    }
}
