namespace BLTC.Services.Data
{
    using System.Collections.Generic;

    public interface IManufacturersService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
