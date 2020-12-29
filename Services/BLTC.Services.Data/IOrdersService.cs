namespace BLTC.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BLTC.Services.Data.Models;

    public interface IOrdersService
    {
        Task<int> CreateAsync(string userId);

        bool IsFinished(string userId);

        Task DecreaseItemAmount(int id);

        void RemoveItemFromCart(int id, string cartId);

        Task ClearCartAsync(string cartId, string orderNumber);

        IEnumerable<CheckoutItemsDto> GetCartItems(string cartId);

        IEnumerable<UsersOrdersDto> GetUsersOrders(string userId);

        Task AddShippingInfo(string userId, string address, string city, string state, int zipCode);
    }
}
