namespace BLTC.Web.ViewModels.Messages
{
    using AutoMapper;
    using BLTC.Data.Models;
    using BLTC.Services.Mapping;

    public class MessageAllViewModel : BaseMessagesViewModel, IMapFrom<ContactMessage>, IHaveCustomMappings
    {
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ContactMessage, MessageAllViewModel>();
        }
    }
}
