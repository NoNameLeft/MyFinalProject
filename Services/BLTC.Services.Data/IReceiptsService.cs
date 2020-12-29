namespace BLTC.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BLTC.Services.Data.Models;

    public interface IReceiptsService
    {
        Task CreateReceipt(string userId);

        IEnumerable<ReceiptsDto> GetReceiptByCustomerId(string userId, string orderNumber);
    }
}
