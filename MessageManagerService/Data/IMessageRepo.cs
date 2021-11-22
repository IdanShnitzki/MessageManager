using MessageManagerService.Controllers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageManagerService.Data
{
    public interface IMessageRepo
    {
        bool SaveChanges();
        Task SaveChangesAsync();
        void UpdateChanges(Message message);

        void CreateMessage(Message message);
        IEnumerable<Message> GetAllMessages();
        Message GetMessageById(int id);
        void RemoveMessage(int id);
        void UpdateMessage(int id, Message message);
    }
}
