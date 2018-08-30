using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MyCarApp.Services.SeedData.Contracts;
using System;

namespace MyCarApp.DataSeed
{
    public static class ApplicationBuilderVehicleExtensions
    {
        public async static void SeedVehicleDatabase(this IApplicationBuilder app)
        {
            var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = serviceFactory.CreateScope();
            using (scope)
            {
                Console.WriteLine("Database seeding....");
                var seedManager = scope.ServiceProvider.GetRequiredService<ISeedDataService>();
                await seedManager.ImportEntities( @"..\Datasets\");
                Console.WriteLine("Database seeded.");
            }
        }        
    }
}
