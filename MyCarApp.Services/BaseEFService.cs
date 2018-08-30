using AutoMapper;
using MyCarApp.Data;

namespace MyCarApp.Services
{
    public abstract class BaseEFService
    {
        protected BaseEFService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        protected ApplicationDbContext DbContext { get; private set; }

        protected IMapper Mapper { get; private set; }
    }
}
