using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Utilities;

namespace Repositories
{
    public class LearningClassRepository : IDisposable
    {
        private readonly WebContext _context=new WebContext();

        public IQueryable<LearningClass> GetAll()
        {
            try
            {
                return _context.LearningClasses;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public LearningClass GetById(int id)
        {
            try
            {
                return _context.LearningClasses.FirstOrDefault(s => s.ClassId == id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public IQueryable<Student> GetStudentList(int id)
        {
            try
            {
                return _context.LearningClassStudents.Where(s => s.ClassId == id).Select(m => m.Student);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool Add(LearningClass learningClass)
        {
            try
            {
                _context.LearningClasses.Add(learningClass);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(LearningClass learningClass)
        {
            try
            {
                _context.Entry(learningClass).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(LearningClass learningClass)
        {
            var curr = GetById(learningClass.ClassId);
            if (curr == null)
                return false;

            try
            {
                _context.LearningClasses.Remove(curr);
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
