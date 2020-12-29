namespace BLTC.Services.Data.Models
{
    using System.Collections.Generic;

    public class CheckoutItemsListDto
    {
        public CheckoutItemsListDto()
        {
            this.CheckoutItems = new HashSet<CheckoutItemsDto>();
        }

        public virtual ICollection<CheckoutItemsDto> CheckoutItems { get; set; }
    }
}
