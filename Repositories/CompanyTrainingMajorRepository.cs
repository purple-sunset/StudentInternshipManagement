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
    public class CompanyTrainingMajorRepository : IDisposable
    {
        private readonly WebContext _context=new WebContext();

        public IQueryable<CompanyTrainingMajor> GetAll()
        {
            try
            {
                return _context.CompanyTrainingMajors;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }
        public CompanyTrainingMajor GetById(int companyId, int trainingMajorId)
        {
            try
            {
                return _context.CompanyTrainingMajors.FirstOrDefault(s => (s.CompanyId == companyId && s.TrainingMajorId == trainingMajorId));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public IQueryable<CompanyTrainingMajor> GetByCompany(int companyId)
        {
            try
            {
                return _context.CompanyTrainingMajors.Where(s => s.CompanyId == companyId);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public IQueryable<CompanyTrainingMajor> GetByTrainingMajor(int trainingMajorId)
        {
            try
            {
                return _context.CompanyTrainingMajors.Where(s => s.TrainingMajorId == trainingMajorId);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool Add(CompanyTrainingMajor companyTrainingMajor)
        {
            try
            {
                _context.CompanyTrainingMajors.Add(companyTrainingMajor);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(CompanyTrainingMajor companyTrainingMajor)
        {
            try
            {
                _context.Entry(companyTrainingMajor).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(CompanyTrainingMajor companyTrainingMajor)
        {
            var curr = GetById(companyTrainingMajor.CompanyId, companyTrainingMajor.TrainingMajorId);
            if (curr == null)
                return false;

            try
            {
                _context.CompanyTrainingMajors.Remove(curr);
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
