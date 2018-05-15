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
    public class StudentRepository
    {
        private readonly WebContext _context=new WebContext();

        public IQueryable<Student> GetAll()
        {
            return _context.Students;
        }

        public Student GetById(string id)
        {
            try
            {
                return _context.Students.Include("Class").FirstOrDefault(s => s.StudentId.Equals(id));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool Add(Student student)
        {
            try
            {
                _context.Students.Add(student);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(Student student)
        {
            try
            {
                _context.Entry(student).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(Student student)
        {
            var curr = GetById(student.StudentId);
            if (curr == null)
                return false;

            try
            {
                _context.Students.Remove(curr);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }
    }
}
