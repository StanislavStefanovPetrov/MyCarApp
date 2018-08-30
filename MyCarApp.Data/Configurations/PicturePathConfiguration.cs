using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCarApp.Models.Vehicles;

namespace MyCarApp.Data.Configurations
{
    public class PicturePathConfiguration : IEntityTypeConfiguration<PicturePath>
    {
        public void Configure(EntityTypeBuilder<PicturePath> builder)
        {
            builder
                 .HasIndex(e => e.Path)
                 .IsUnique(true);
        }
    }
}
