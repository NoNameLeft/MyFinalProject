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
        private readonly string defaultPassword = "admin123";
        private readonly string defaultPhone = "+359884917675";

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var owner = new ApplicationUser
            {
                UserName = GlobalConstants.DefaultOwner,
                NormalizedUserName = GlobalConstants.DefaultOwner.ToUpper(),
                Email = GlobalConstants.DefaultOwner,
                NormalizedEmail = GlobalConstants.DefaultOwner.ToUpper(),
                PhoneNumber = this.defaultPhone,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
            };

            var password = new PasswordHasher<ApplicationUser>();
            owner.PasswordHash = password.HashPassword(owner, this.defaultPassword);

            await SeedUserAsync(userManager, owner, this.defaultPassword);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, ApplicationUser owner, string password)
        {
            var user = await userManager.FindByNameAsync(GlobalConstants.DefaultOwner);
            if (user == null)
            {
                var userResult = await userManager.CreateAsync(owner, password);
                var userToRoleResult = await userManager.AddToRoleAsync(owner, GlobalConstants.AdministratorRoleName);
                if (!userResult.Succeeded ||
                    !userToRoleResult.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, userResult.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
