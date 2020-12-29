namespace BLTC.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Data.Common.Repositories;
    using BLTC.Data.Models;
    using BLTC.Services.Data.Models;
    using BLTC.Services.Mapping;

    public class MessagesService : IMessagesService
    {
        private readonly IDeletableEntityRepository<ContactMessage> messagesRepository;

        public MessagesService(IDeletableEntityRepository<ContactMessage> messagesRepository)
        {
            this.messagesRepository = messagesRepository;
        }

        public async Task CreateMessageAsync(MessagesDto dto)
        {
            if (this.messagesRepository.All().Where(x => x.Email == dto.Email).Count() <= 1)
            {
                var msg = new ContactMessage
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Subject = dto.Subject,
                };

                await this.messagesRepository.AddAsync(msg);
                await this.messagesRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("You cannot have more than 2 unanswered messages.");
            }
        }

        public IEnumerable<T> GetMessages<T>()
        {
            return this.messagesRepository.All().To<T>().AsEnumerable();
        }

        public async Task RemoveMessage(string id)
        {
            var msg = this.messagesRepository.All().SingleOrDefault(x => x.Id == id);

            this.messagesRepository.Delete(msg);
            await this.messagesRepository.SaveChangesAsync();
        }
    }
}
