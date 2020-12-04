namespace BLTC.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Common;
    using BLTC.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = new ApplicationUser
            {
                UserName = "Owner",
                NormalizedUserName = "OWNER",
                Email = "admin@admin.com",
                NormalizedEmail = "admin@admin.com",
                PhoneNumber = "+359884917675",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(user, "secret");
            user.PasswordHash = hashed;

            await userManager.CreateAsync(user);
            await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
        }
    }
}
