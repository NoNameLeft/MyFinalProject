namespace BLTC.Web.ViewModels.Items
{
    using AutoMapper;
    using BLTC.Data.Models;
    using BLTC.Services.Mapping;

    public class ItemsAllViewModel : BaseItemViewModel, IMapFrom<Item>, IHaveCustomMappings
    {
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Item, ItemsAllViewModel>();
        }
    }
}
