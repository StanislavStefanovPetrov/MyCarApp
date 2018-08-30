using AutoMapper;
using MyCarApp.Web.Mapping;

namespace MyCarApp.Tests.Mocks
{
    
    public static class MockAutoMapper
    {
        private static readonly IMapper _myInstance = GetMapper();

        public static IMapper GetMapper()
        {
            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
            var mapper = Mapper.Instance;
            return mapper;
        }

        public static IMapper Instance
        {
            get { return _myInstance; }
        }
    }
}
