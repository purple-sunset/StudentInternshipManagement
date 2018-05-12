using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class StudentService
    {
        private readonly StudentRepository _repository=new StudentRepository();

        public Student GetById(string id)
        {
            return _repository.GetById(id);
        }

        public bool Add(Student student)
        {
            return _repository.Add(student);
        }

        public bool Update(Student student)
        {
            return _repository.Update(student);
        }

        public bool Delete(Student student)
        {
            return _repository.Delete(student);
        }

    }
}
