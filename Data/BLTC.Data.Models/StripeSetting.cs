namespace BLTC.Data.Models
{
    using BLTC.Data.Common.Models;

    public class StripeSetting : BaseDeletableModel<int>
    {
        public string SecretKey { get; set; }

        public string PublishableKey { get; set; }
    }
}
