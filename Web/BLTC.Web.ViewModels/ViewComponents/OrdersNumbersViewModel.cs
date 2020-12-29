namespace BLTC.Web.ViewModels.ViewComponents
{
    using System;
    using System.Collections.Generic;

    using BLTC.Data.Models;
    using BLTC.Data.Models.Enums;
    using BLTC.Services.Mapping;

    public class OrdersNumbersViewModel
    {
        public string Address { get; set; }

        public string City { get; set; }

        public int ZipCode { get; set; }

        public string Number { get; set; }

        public OrderStatus Status { get; set; }
    }
}
