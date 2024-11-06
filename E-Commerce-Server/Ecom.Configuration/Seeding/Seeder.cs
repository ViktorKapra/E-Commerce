using ECom.Data.Account;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ECom.Configuration.Seeding
{
    public class Seeder
    {
        public static async void Seed(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<EComRole>>();
                var roles = new[] { "Admin", "User" };
                foreach (var role in roles)
                {
                    {
                        if (!await roleManager.RoleExistsAsync(role))
                        {
                            await roleManager.CreateAsync(new EComRole(role));
                        }

                    }
                }
            }
        }
    }
}
