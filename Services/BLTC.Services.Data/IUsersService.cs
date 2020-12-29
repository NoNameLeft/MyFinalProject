namespace BLTC.Services.Data
{
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<string> GetIdAsync(string username);

        Task<string> GetOrderNumber(string username);
    }
}
