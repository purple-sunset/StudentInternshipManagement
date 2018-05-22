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
    public class NotificationRepository:IDisposable
    {
        private readonly WebContext _context = new WebContext();

        public IQueryable<Notification> GetAll()
        {
            try
            {
                return _context.Notifications;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }
        public Notification GetById(int id)
        {
            try
            {
                return _context.Notifications.FirstOrDefault(s => s.NotificationId == id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool Add(Notification notification)
        {
            try
            {
                _context.Notifications.Add(notification);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(Notification notification)
        {
            try
            {
                _context.Entry(notification).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(Notification notification)
        {
            var curr = GetById(notification.NotificationId);
            if (curr == null)
                return false;

            try
            {
                _context.Notifications.Remove(curr);
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
