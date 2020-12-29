namespace BLTC.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BLTC.Services.Data.Models;

    public interface IMessagesService
    {
        Task CreateMessageAsync(MessagesDto dto);

        Task RemoveMessage(string id);

        IEnumerable<T> GetMessages<T>();
    }
}
