namespace BLTC.Services.Data.Models
{
    using System.Collections.Generic;

    public class CartsItemsListDto
    {
        public CartsItemsListDto()
        {
            this.CartsItems = new HashSet<CartsItemsDto>();
        }

        public virtual ICollection<CartsItemsDto> CartsItems { get; set; }
    }
}
