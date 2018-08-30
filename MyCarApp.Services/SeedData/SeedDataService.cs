using AutoMapper;
using MyCarApp.Data;
using MyCarApp.Services.SeedData.Contracts;
using MyCatApp.DataProcessor;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MyCarApp.Services.SeedData
{
    public class SeedDataService : BaseEFService, ISeedDataService
    {
        public SeedDataService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            
        }

        public async Task ImportEntities(string baseDir = @"..\Datasets\")
        {
            using (this.DbContext)
            {
                using (var transaction = this.DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        Deserializer.ImportColours(this.DbContext, baseDir, "colours.json");
                        Deserializer.ImportFuelTypes(this.DbContext, baseDir, "fuelTypes.json");
                        Deserializer.ImportTransmissions(this.DbContext, baseDir, "transmissions.json");
                        Deserializer.ImportVehicleConditions(this.DbContext, baseDir, "vehicleConditions.json");
                        Deserializer.ImportVehicleMakes(this.DbContext, baseDir, "vehicleMakes.json");
                        Deserializer.ImportVehicleTypes(this.DbContext, baseDir, "vehicleTypes.json");
                        Deserializer.ImportPictureValidExtensions(this.DbContext, baseDir, "pictureExtensions.json");
                        await this.DbContext.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException($"Invalid operation - seeding Data base: {ex.Message}");
                    }
                }
            }
        }
    }
}
