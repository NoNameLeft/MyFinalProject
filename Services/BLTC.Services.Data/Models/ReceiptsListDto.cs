namespace BLTC.Services.Data.Models
{
    using System.Collections.Generic;

    public class ReceiptsListDto
    {
        public ReceiptsListDto()
        {
            this.Receipts = new HashSet<ReceiptsDto>();
        }

        public virtual ICollection<ReceiptsDto> Receipts { get; set; }
    }
}
