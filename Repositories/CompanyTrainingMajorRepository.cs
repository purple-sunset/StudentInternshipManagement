using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repositories
{
    public class CompanyTrainingMajorRepository : IDisposable
    {
        private readonly WebContext _context=new WebContext();

        public IQueryable<CompanyTrainingMajor> GetAll()
        {
            return _context.CompanyTrainingMajors;
        }
        public CompanyTrainingMajor GetById(int companyId, int trainingMajorId)
        {
            return _context.CompanyTrainingMajors.FirstOrDefault(s => (s.CompanyId==companyId && s.TrainingMajorId== trainingMajorId));
        }

        public IQueryable<CompanyTrainingMajor> GetByCompany(int companyId)
        {
            return _context.CompanyTrainingMajors.Where(s => s.CompanyId == companyId);
        }

        public IQueryable<CompanyTrainingMajor> GetByTrainingMajor(int trainingMajorId)
        {
            return _context.CompanyTrainingMajors.Where(s => s.TrainingMajorId == trainingMajorId);
        }

        public bool Add(CompanyTrainingMajor companyTrainingMajor)
        {
            _context.Entry(companyTrainingMajor).State = EntityState.Added;
            return _context.SaveChanges() > 0;
        }

        public bool Update(CompanyTrainingMajor companyTrainingMajor)
        {
            _context.Entry(companyTrainingMajor).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public bool Delete(CompanyTrainingMajor companyTrainingMajor)
        {
            _context.Entry(companyTrainingMajor).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
