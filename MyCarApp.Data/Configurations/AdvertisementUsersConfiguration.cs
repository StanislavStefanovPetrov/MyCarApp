using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCarApp.Models.Advertisements;

namespace MyCarApp.Data.Configurations
{
    public class AdvertisementUsersConfiguration : IEntityTypeConfiguration<AdvertisementUser>
    {
        public void Configure(EntityTypeBuilder<AdvertisementUser> builder)
        {
            builder.HasKey(e => new { e.UserId, e.AdvertisementId });

            builder.HasOne(e => e.User)
                .WithMany(e => e.TrackAdvertisements)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Advertisement)
                .WithMany(g => g.Watchers)
                .HasForeignKey(e => e.AdvertisementId);
        }
    }
}
