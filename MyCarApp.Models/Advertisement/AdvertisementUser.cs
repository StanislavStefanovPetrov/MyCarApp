namespace MyCarApp.Models.Advertisements
{
    public class AdvertisementUser
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
    }
}
