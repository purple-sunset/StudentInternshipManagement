using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repositories
{
    public class TrainingMajorRepository : IDisposable
    {
        private readonly WebContext _context=new WebContext();

        public IQueryable<TrainingMajor> GetAll()
        {
            return _context.TrainingMajors;
        }
        public TrainingMajor GetById(int id)
        {
            return _context.TrainingMajors.FirstOrDefault(s => s.TrainingMajorId==id);
        }

        public bool Add(TrainingMajor trainingMajor)
        {
            _context.Entry(trainingMajor).State = EntityState.Added;
            return _context.SaveChanges() > 0;
        }

        public bool Update(TrainingMajor trainingMajor)
        {
            _context.Entry(trainingMajor).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public bool Delete(TrainingMajor trainingMajor)
        {
            _context.Entry(trainingMajor).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
