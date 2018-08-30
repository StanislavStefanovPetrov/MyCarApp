using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCarApp.Models.Vehicles;

namespace MyCarApp.Data.Configurations
{
    public class VehicleMakeConfiguration : IEntityTypeConfiguration<VehicleMake>
    {
        public void Configure(EntityTypeBuilder<VehicleMake> builder)
        {
            builder
                 .HasIndex(e => e.Name)
                 .IsUnique(true);
        }
    }
}
