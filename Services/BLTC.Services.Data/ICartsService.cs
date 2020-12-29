namespace BLTC.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BLTC.Services.Data.Models;

    public interface ICartsService
    {
        Task CreateAsync(string userId);

        Task AddItemToCart(int orderId, int itemId, string userId);

        string GetByUserId(string userId);

        bool HasCart(string userId);

        IEnumerable<CartsItemsDto> GetCartsItemsAsync(string userId);
    }
}
