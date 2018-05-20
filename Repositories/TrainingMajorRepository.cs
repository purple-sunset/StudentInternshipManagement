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
    public class TrainingMajorRepository : IDisposable
    {
        private readonly WebContext _context=new WebContext();

        public IQueryable<TrainingMajor> GetAll()
        {
            try
            {
                return _context.TrainingMajors;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }
        public TrainingMajor GetById(int id)
        {
            try
            {
                return _context.TrainingMajors.FirstOrDefault(s => s.TrainingMajorId == id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool Add(TrainingMajor trainingMajor)
        {
            try
            {
                _context.TrainingMajors.Add(trainingMajor);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(TrainingMajor trainingMajor)
        {
            try
            {
                _context.Entry(trainingMajor).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(TrainingMajor trainingMajor)
        {
            var curr = GetById(trainingMajor.TrainingMajorId);
            if (curr == null)
                return false;

            try
            {
                _context.TrainingMajors.Remove(curr);
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
