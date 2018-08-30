using System.Threading.Tasks;

namespace MyCarApp.Services.SeedData.Contracts
{
    public interface ISeedDataService
    {
        Task ImportEntities(string baseDir);
    }
}
