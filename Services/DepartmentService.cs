using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class DepartmentService
    {
        private readonly DepartmentRepository _repository=new DepartmentRepository();

        public Department GetById(string id)
        {
            return _repository.GetById(id);
        }

        public bool Add(Department department)
        {
            return _repository.Add(department);
        }

        public bool Update(Department department)
        {
            return _repository.Update(department);
        }

        public bool Delete(Department department)
        {
            return _repository.Delete(department);
        }

    }
}
