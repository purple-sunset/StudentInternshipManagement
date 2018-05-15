using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class AdminService
    {
        private readonly AdminRepository _repository=new AdminRepository();

        public IQueryable<Admin> GetAll()
        {
            return _repository.GetAll();
        }
        public Admin GetById(string id)
        {
            return _repository.GetById(id);
        }

        public bool Add(Admin admin)
        {
            return _repository.Add(admin);
        }

        public bool Update(Admin admin)
        {
            return _repository.Update(admin);
        }

        public bool Delete(Admin admin)
        {
            return _repository.Delete(admin);
        }

    }
}
