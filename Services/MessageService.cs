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

        public IQueryable<Message> GetByTeacher(string id)
        {
            return _repository.GetAll().Where(m => m.TeacherId.Equals(id)).OrderByDescending(m=>m.Time);
        }

        public IQueryable<Message> GetReceivedByTeacher(string id)
        {
            return GetByTeacher(id).Where(m=>m.Type==1);
        }

        public IQueryable<Message> GetSentByTeacher(string id)
        {
            return GetByTeacher(id).Where(m => m.Type == 2);
        }

        public IQueryable<Message> GetDraftByTeacher(string id)
        {
            return GetByTeacher(id).Where(m => m.Type == 3);
        }

        public IQueryable<Message> GetByStudent(string id)
        {
            return _repository.GetAll().Where(m => m.StudentId.Equals(id)).OrderByDescending(m => m.Time);
        }

        public IQueryable<Message> GetReceivedByStudent(string id)
        {
            return GetByStudent(id).Where(m => m.Type == 1);
        }

        public IQueryable<Message> GetSentByStudent(string id)
        {
            return GetByStudent(id).Where(m => m.Type == 2);
        }

        public IQueryable<Message> GetDraftByStudent(string id)
        {
            return GetByStudent(id).Where(m => m.Type == 3);
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
