namespace BLTC.Services.Data
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using BLTC.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> usersManager;

        public UsersService(UserManager<ApplicationUser> usersManger)
        {
            this.usersManager = usersManger;
        }

        public async Task<string> GetUserIdByUsername(string username)
        {
            var user = await this.usersManager.FindByNameAsync(username);

            return user.Id;
        }

        private string GetUserId(ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
        }
    }
}
