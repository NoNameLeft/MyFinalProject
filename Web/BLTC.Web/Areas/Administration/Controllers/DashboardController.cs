namespace BLTC.Web.Areas.Administration.Controllers
{
    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.Administration.Dashboard;
    using BLTC.Web.ViewModels.Messages;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IMessagesService messagesService;

        public DashboardController(
            ISettingsService settingsService,
            IMessagesService messagesService)
        {
            this.settingsService = settingsService;
            this.messagesService = messagesService;
        }

        public IActionResult Index()
        {
            var messages = this.messagesService.GetMessages<MessageAllViewModel>();
            var viewModel = new IndexViewModel { Messages = messages };
            return this.View(viewModel);
        }
    }
}
