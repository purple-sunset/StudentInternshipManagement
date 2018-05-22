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
    public class StudentRepository : IDisposable
    {
        private readonly WebContext _context=new WebContext();

        public IQueryable<Student> GetAll()
        {
            try
            {
                return _context.Students;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public IQueryable<Student> GetByStudentClass(int classId)
        {
            try
            {
                return _context.Students.Where(s => s.ClassId == classId);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public Student GetById(string id)
        {
            try
            {
                return _context.Students.FirstOrDefault(s => s.StudentId.Equals(id));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public IQueryable<LearningClass> GetLearningClassList(int id)
        {
            try
            {
                return _context.LearningClassStudents.Where(s => s.StudentId.Equals(id)).Select(m => m.Class);
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

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
