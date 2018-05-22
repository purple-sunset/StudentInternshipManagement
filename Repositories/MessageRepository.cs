using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Utilities;

namespace Repositories
{
    public class MessageRepository:IDisposable
    {
        private readonly WebContext _context = new WebContext();

        public IQueryable<Message> GetAll()
        {
            try
            {
                return _context.Messages;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }
        public Message GetById(int id)
        {
            try
            {
                return _context.Messages.FirstOrDefault(s => s.MessageId == id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool Add(Message message)
        {
            try
            {
                _context.Messages.Add(message);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(Message message)
        {
            try
            {
                _context.Entry(message).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(Message message)
        {
            var curr = GetById(message.MessageId);
            if (curr == null)
                return false;

            try
            {
                _context.Messages.Remove(curr);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
