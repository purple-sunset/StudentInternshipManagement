using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class StatisticService : IDisposable
    {
        private readonly StatisticRepository _repository=new StatisticRepository();

        public IQueryable<Statistic> GetAll()
        {
            return _repository.GetAll();
        }
        public Statistic GetById(int id)
        {
            return _repository.GetById(id);
        }

        public bool Add(Statistic statistic)
        {
            return _repository.Add(statistic);
        }

        public bool Update(Statistic statistic)
        {
            return _repository.Update(statistic);
        }

        public bool Delete(Statistic statistic)
        {
            return _repository.Delete(statistic);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
