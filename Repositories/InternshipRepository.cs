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
    public class InternshipRepository:IDisposable
    {
        private readonly WebContext _context = new WebContext();

        public IQueryable<Internship> GetAll()
        {
            try
            {
                return _context.Internships;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public IQueryable<Internship> GetBySemester(int semesterId)
        {
            try
            {
                return _context.Internships.Where(i=>i.Class.SemesterId == semesterId);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public IQueryable<Internship> GetByStudent(string studentId)
        {
            try
            {
                return _context.Internships.Where(i => i.Student.StudentId.Equals(studentId));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public Internship GetById(int id)
        {
            try
            {
                return _context.Internships.FirstOrDefault(s => s.InternshipId == id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool Add(Internship internship)
        {
            try
            {
                _context.Internships.Add(internship);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(Internship internship)
        {
            try
            {
                _context.Entry(internship).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(Internship internship)
        {
            var curr = GetById(internship.InternshipId);
            if (curr == null)
                return false;

            try
            {
                _context.Internships.Remove(curr);
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
