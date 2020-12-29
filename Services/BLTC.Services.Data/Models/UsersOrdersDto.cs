namespace BLTC.Services.Data.Models
{
    using BLTC.Data.Models.Enums;

    public class UsersOrdersDto
    {
        public string Address { get; set; }

        public string City { get; set; }

        public int ZipCode { get; set; }

        public string Number { get; set; }

        public OrderStatus Status { get; set; }
    }
}
