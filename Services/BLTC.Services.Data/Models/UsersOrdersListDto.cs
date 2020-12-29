namespace BLTC.Services.Data.Models
{
    using System.Collections.Generic;

    public class UsersOrdersListDto
    {
        public UsersOrdersListDto()
        {
            this.UsersOrders = new HashSet<UsersOrdersDto>();
        }

        public virtual ICollection<UsersOrdersDto> UsersOrders { get; set; }
    }
}
