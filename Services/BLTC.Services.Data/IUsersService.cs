namespace BLTC.Services.Data
{
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<string> GetUserIdByUsername(string username);
    }
}
