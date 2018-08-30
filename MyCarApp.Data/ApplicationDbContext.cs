using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCarApp.Data.Configurations;
using MyCarApp.Models;
using MyCarApp.Models.Advertisements;
using MyCarApp.Models.Vehicles;

namespace MyCarApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Advertisement> Advertisements { get; set; }

        public DbSet<AdvertisementUser> AdvertisementUsers { get; set; }

        public DbSet<Colour> Colours { get; set; }

        public DbSet<Engine> Engines { get; set; }

        public DbSet<EngineFuelType> EngineFuelTypes { get; set; }

        public DbSet<FuelType> FuelTypes { get; set; }

        public DbSet<Transmission> Transmissions { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<VehicleCondition> VehicleConditions { get; set; }

        public DbSet<VehicleMake> VehicleMakes { get; set; }

        public DbSet<VehicleModel> VehicleModels { get; set; }

        public DbSet<VehicleType> VehicleTypes { get; set; }

        public DbSet<PicturePath> PicturePaths { get; set; }

        public DbSet<PictureValidExtensions> PictureValidExtensions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AdvertisementUsersConfiguration());
            builder.ApplyConfiguration(new EngineFuelTypesConfiguration());

            builder.ApplyConfiguration(new ColourConfiguration());
            builder.ApplyConfiguration(new FuelTypeConfiguration());
            builder.ApplyConfiguration(new EngineConfiguration());
            builder.ApplyConfiguration(new VehicleConditionConfiguration());
            builder.ApplyConfiguration(new VehicleModelConfiguration());
            builder.ApplyConfiguration(new VehicleMakeConfiguration());
            builder.ApplyConfiguration(new VehicleTypeConfiguration());
            builder.ApplyConfiguration(new PicturePathConfiguration());
            builder.ApplyConfiguration(new PictureExtensionsConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
