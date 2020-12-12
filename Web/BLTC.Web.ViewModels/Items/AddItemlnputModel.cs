namespace BLTC.Web.ViewModels.Items
{
    using System.ComponentModel.DataAnnotations;

    public class AddItemlnputModel : ItemBaseInputModel
    {
        [Required]
        public int Manufacturer { get; set; }
    }
}
