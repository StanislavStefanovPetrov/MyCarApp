using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCarApp.Models.Vehicles;

namespace MyCarApp.Data.Configurations
{
    public class ColourConfiguration : IEntityTypeConfiguration<Colour>
    {
        public void Configure(EntityTypeBuilder<Colour> builder)
        {
            builder
                 .HasIndex(e => e.Name)
                 .IsUnique(true);
        }
    }
}
