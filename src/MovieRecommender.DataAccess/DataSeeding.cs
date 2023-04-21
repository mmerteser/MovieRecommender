using Microsoft.EntityFrameworkCore;
using MovieRecommender.Application;
using MovieRecommender.Application.Utilities.Security.Hashing;
using MovieRecommender.Core.Entities;
using MovieRecommender.DataAccess.Context;

namespace MovieRecommender.DataAccess
{
    public class DataSeeding
    {
        public async Task SeedAsync()
        {
            var dbContextBuilder = new DbContextOptionsBuilder()
                .UseSqlServer(Configuration.ConnectionString);

            var context = new MovieDbContext(dbContextBuilder.Options);

            await context.Database.EnsureCreatedAsync();

            var user = await context.Users.SingleOrDefaultAsync(i => i.Username.Equals("admin"));

            if (user is null)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash("admin", out passwordHash, out passwordSalt);

                await context.Users.AddAsync(
                    new User
                    {
                        Username = "admin",
                        FirstLastName = "admin admin",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    });

            }
            await context.SaveChangesAsync();

        }
    }
}
