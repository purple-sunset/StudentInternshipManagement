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
    public class StatisticRepository:IDisposable
    {
        private readonly WebContext _context = new WebContext();

        public IQueryable<Statistic> GetAll()
        {
            try
            {
                return _context.Statistics;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }
        public Statistic GetById(int id)
        {
            try
            {
                return _context.Statistics.FirstOrDefault(s => s.StatisticId == id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool Add(Statistic statistic)
        {
            try
            {
                _context.Statistics.Add(statistic);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(Statistic statistic)
        {
            try
            {
                _context.Entry(statistic).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(Statistic statistic)
        {
            var curr = GetById(statistic.StatisticId);
            if (curr == null)
                return false;

            try
            {
                _context.Statistics.Remove(curr);
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
