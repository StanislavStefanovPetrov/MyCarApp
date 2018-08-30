using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCarApp.Models.Vehicles;

namespace MyCarApp.Data.Configurations
{
    public class PictureExtensionsConfiguration : IEntityTypeConfiguration<PictureValidExtensions>
    {
        public void Configure(EntityTypeBuilder<PictureValidExtensions> builder)
        {
            builder
                 .HasIndex(e => e.Extension)
                 .IsUnique(true);
        }
    }
}
