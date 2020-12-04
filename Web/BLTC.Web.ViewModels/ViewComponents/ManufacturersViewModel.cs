namespace BLTC.Web.ViewModels.ViewComponents
{
    using System.Collections.Generic;

    public class ManufacturersViewModel
    {
        public IEnumerable<KeyValuePair<string, string>> Manufacturers { get; set; }
    }
}
