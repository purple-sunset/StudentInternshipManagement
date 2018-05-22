using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class NotificationService : IDisposable
    {
        private readonly NotificationRepository _repository=new NotificationRepository();

        public IQueryable<Notification> GetAll()
        {
            return _repository.GetAll();
        }
        public Notification GetById(int id)
        {
            return _repository.GetById(id);
        }

        public bool Add(Notification notification)
        {
            return _repository.Add(notification);
        }

        public bool Update(Notification notification)
        {
            return _repository.Update(notification);
        }

        public bool Delete(Notification notification)
        {
            return _repository.Delete(notification);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
