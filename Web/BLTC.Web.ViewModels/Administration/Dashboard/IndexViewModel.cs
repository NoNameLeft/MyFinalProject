namespace BLTC.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    using BLTC.Web.ViewModels.Messages;

    public class IndexViewModel
    {
        public IEnumerable<MessageAllViewModel> Messages { get; set; }
    }
}
