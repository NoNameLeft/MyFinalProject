namespace BLTC.Web.Controllers
{
    using System.Threading.Tasks;

    using BLTC.Common;
    using BLTC.Services.Data;
    using BLTC.Services.Data.Models;
    using BLTC.Web.ViewModels.Messages;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class MessagesController : BaseController
    {
        private readonly IMessagesService messagesService;

        public MessagesController(IMessagesService messagesService)
        {
            this.messagesService = messagesService;
        }

        [AllowAnonymous]
        public IActionResult ContactUs()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ContactUs(MessageCreateInputModel input)
        {
            var dto = new MessagesDto
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Email = input.Email,
                Subject = input.Subject,
            };

            await this.messagesService.CreateMessageAsync(dto);
            return this.Redirect("/");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Resolve(string messageId)
        {
            await this.messagesService.RemoveMessage(messageId);
            return this.Redirect("/Administration/Dashboard");
        }
    }
}
