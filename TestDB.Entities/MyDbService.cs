using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDB.Entities
{
    public class MyDbService
    {
        private static Guid _userId = new Guid("D0F56CC0-E232-4BA2-9C2A-D5E099E74EAD");
        private readonly IServiceScopeFactory _scopeFactory;

        public MyDbService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task AddUserAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var dbc = scope.ServiceProvider.GetRequiredService<MyDbContext>();

            if (!dbc.Users.Any(x => x.Id == _userId))
            {
                dbc.Users.Add(new User
                {
                    Id = _userId,
                    Email = "test@test.com"
                });
                await dbc.SaveChangesAsync();
            }
        }

        public async Task<User?> GetUserAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var dbc = scope.ServiceProvider.GetRequiredService<MyDbContext>();
            return await dbc.Users.FirstOrDefaultAsync(x => x.Id == _userId);
        }
    }
}
