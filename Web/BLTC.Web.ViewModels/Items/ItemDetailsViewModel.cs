namespace BLTC.Web.ViewModels.Items
{
    using System.ComponentModel;

    using AutoMapper;
    using BLTC.Data.Models;
    using BLTC.Services.Mapping;

    public class ItemDetailsViewModel : BaseItemViewModel, IMapFrom<Item>, IHaveCustomMappings
    {
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Item, ItemDetailsViewModel>();
        }
    }
}
