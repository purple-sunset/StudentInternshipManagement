using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class GroupService : IDisposable
    {
        private readonly GroupRepository _repository=new GroupRepository();

        public IQueryable<Group> GetAll()
        {
            return _repository.GetAll();
        }
        public Group GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Group> GetByStudent(string id)
        {
            return _repository.GetAll().Where(g=>g.Members.Select(s=>s.StudentId).Contains(id));
        }

        public Group GetByInternship(Internship internship)
        {
            return _repository.GetAll().FirstOrDefault(g =>
                g.ClassId == internship.ClassId && g.Members.Select(s => s.StudentId).Contains(internship.StudentId));
        }

        public IQueryable<Group> GetByTeacher(string id)
        {
            return _repository.GetAll().Where(g => g.TeacherId.Equals(id));
        }

        public bool Add(Group group)
        {
            return _repository.Add(group);
        }

        public bool Update(Group group)
        {
            return _repository.Update(group);
        }

        public bool Delete(Group group)
        {
            return _repository.Delete(group);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
