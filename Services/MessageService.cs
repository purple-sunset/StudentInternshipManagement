using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class MessageService : IDisposable
    {
        private readonly MessageRepository _repository=new MessageRepository();

        public IQueryable<Message> GetAll()
        {
            return _repository.GetAll();
        }
        public Message GetById(int id)
        {
            return _repository.GetById(id);
        }

        public bool Add(Message message)
        {
            return _repository.Add(message);
        }

        public bool Update(Message message)
        {
            return _repository.Update(message);
        }

        public bool Delete(Message message)
        {
            return _repository.Delete(message);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
