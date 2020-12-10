namespace BLTC.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Data.Common.Repositories;
    using BLTC.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<string> GetUserIdByUsername(string username)
        {
            return await Task.FromResult(this.usersRepository.AllAsNoTracking().Where(x => x.UserName == username).FirstOrDefault().Id);
        }
    }
}
