using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCarApp.Models.Vehicles;

namespace MyCarApp.Data.Configurations
{
    public class VehicleConditionConfiguration : IEntityTypeConfiguration<VehicleCondition>
    {
        public void Configure(EntityTypeBuilder<VehicleCondition> builder)
        {
            builder
                 .HasIndex(e => e.Condition)
                 .IsUnique(true);
        }
    }
}
