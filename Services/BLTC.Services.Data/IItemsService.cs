namespace BLTC.Services.Data
{
    using System;
    using System.Collections.Generic;

    public interface IItemsService
    {
        void Add(string name, int type, int shape, decimal weight, decimal purity, int fineness, int quantity, string dimensions, string description, int manufacturer);

        IEnumerable<KeyValuePair<string, string>> GetKeyValuesOfEnum(Type type);
    }
}
