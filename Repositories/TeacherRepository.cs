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
    public class TeacherRepository
    {
        private readonly WebContext _context=new WebContext();

        public Teacher GetById(string id)
        {
            try
            {
                return _context.Teachers.Include("Department").FirstOrDefault(s => s.TeacherId.Equals(id));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool Add(Teacher teacher)
        {
            try
            {
                _context.Teachers.Add(teacher);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(Teacher teacher)
        {
            try
            {
                _context.Entry(teacher).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(Teacher teacher)
        {
            var curr = GetById(teacher.TeacherId);
            if (curr == null)
                return false;

            try
            {
                _context.Teachers.Remove(curr);
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
