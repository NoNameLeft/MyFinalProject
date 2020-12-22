namespace BLTC.Services.Data
{
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

        public async Task<string> GetIdAsync(string username)
        {
            var user = await this.usersManager.FindByNameAsync(username);

            return user.Id;
        }
    }
}
