using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repositories
{
    public class StudentRepository
    {
        private readonly WebContext _context=new WebContext();

        public Student GetById(string id)
        {
            return _context.Students.FirstOrDefault(s => s.StudentId.Equals(id));
        }

        public bool Update(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }
    }
}
