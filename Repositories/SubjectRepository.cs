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
    public class SubjectRepository : IDisposable
    {
        private readonly WebContext _context=new WebContext();

        public IQueryable<Subject> GetAll()
        {
            try
            {
                return _context.Subjects;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }
        public Subject GetById(string id)
        {
            try
            {
                return _context.Subjects.FirstOrDefault(s => s.SubjectId.Equals(id));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool Add(Subject subject)
        {
            try
            {
                _context.Subjects.Add(subject);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(Subject subject)
        {
            try
            {
                _context.Entry(subject).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(Subject subject)
        {
            var curr = GetById(subject.SubjectId);
            if (curr == null)
                return false;

            try
            {
                _context.Subjects.Remove(curr);
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
