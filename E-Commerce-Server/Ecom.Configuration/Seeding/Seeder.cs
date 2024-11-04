using ECom.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
