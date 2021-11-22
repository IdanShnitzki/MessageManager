using MessageManagerService.Controllers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageManagerService.Data
{
    public class MessageRepo : IMessageRepo
    {
        private readonly AppDbContext _context;

        public MessageRepo(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Message> GetAllMessages()
        {
            return _context.Messages;
        }

        public Message GetMessageById(int id)
        {
            return _context.Messages.FirstOrDefault(m => m.Id == id);
        }

        public void CreateMessage(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            _context.Messages.Add(message);
        }

        public void UpdateMessage(int id, Message message)
        {
            var messageToUpdate = _context.Messages.FirstOrDefault(m => m.Id == id);

            if (messageToUpdate != null)
            {
                messageToUpdate.MessageStr = message.MessageStr;
            }
        }

        public void RemoveMessage(int id)
        {
            var message = _context.Messages.FirstOrDefault(m => m.Id == id);
            _context.Messages.Remove(message);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateChanges(Message message)
        {
            var local = _context.Set<Message>().Local.FirstOrDefault(e => e.Id.Equals(message.Id));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Entry(message).State = EntityState.Modified;

            _context.Update(message);
        }
    }
}
