namespace BLTC.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Data.Common.Repositories;
    using BLTC.Data.Models;
    using BLTC.Data.Models.Enums;
    using Microsoft.AspNetCore.Identity;

    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> usersManager;
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        public UsersService(
            UserManager<ApplicationUser> usersManger,
            IDeletableEntityRepository<Order> ordersRepository)
        {
            this.usersManager = usersManger;
            this.ordersRepository = ordersRepository;
        }

        public async Task<string> GetIdAsync(string username)
        {
            var user = await this.usersManager.FindByNameAsync(username);

            return user.Id;
        }

        public async Task<string> GetOrderNumber(string username)
        {
            var userId = await this.GetIdAsync(username);
            return this.ordersRepository.All().SingleOrDefault(x => x.UserId == userId && x.Status == OrderStatus.InProgress).Number;
        }
    }
}
