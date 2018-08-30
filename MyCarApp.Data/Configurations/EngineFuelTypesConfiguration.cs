using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCarApp.Models.Vehicles;

namespace MyCarApp.Data.Configurations
{
    public class EngineFuelTypesConfiguration : IEntityTypeConfiguration<EngineFuelType>
    {
        public void Configure(EntityTypeBuilder<EngineFuelType> builder)
        {
            builder.HasKey(e => new { e.EngineId, e.FuelTypeId });

            builder.HasOne(e => e.Engine)
                .WithMany(e => e.FuelTypes)
                .HasForeignKey(e => e.EngineId);

            builder.HasOne(e => e.FuelType)
                .WithMany(g => g.Engines)
                .HasForeignKey(e => e.FuelTypeId);
        }
    }
}
