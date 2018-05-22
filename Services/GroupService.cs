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
