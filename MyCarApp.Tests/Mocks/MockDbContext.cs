using Microsoft.EntityFrameworkCore;
using MyCarApp.Data;
using System;

namespace MyCarApp.Tests.Mocks
{
    public static class MockDbContext
    {
        public static ApplicationDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                           .UseInMemoryDatabase(Guid.NewGuid().ToString())
                           .Options;

            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }
    }
}
