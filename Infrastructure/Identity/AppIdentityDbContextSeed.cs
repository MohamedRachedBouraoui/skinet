using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Any() == false)
            {
                var user = GetUser();
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }

        private static AppUser GetUser()
        {
            return new AppUser
            {
                DisplayName = "Rached",
                UserName = "mrb@test.com",
                Email = "mrb@test.com",
                Address = new Address
                {
                    FirstName = "Rached",
                    LastName = "Bouraoui",
                    Street = "Chemin des 4 Bourgeois",
                    City = "Ville de Québec",
                    State = "QC",
                    Zipcode = "G1W2K5"
                }
            };
        }
    }
}