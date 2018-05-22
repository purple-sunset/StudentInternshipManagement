using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class NewsService : IDisposable
    {
        private readonly NewsRepository _repository=new NewsRepository();

        public IQueryable<News> GetAll()
        {
            return _repository.GetAll();
        }
        public News GetById(int id)
        {
            return _repository.GetById(id);
        }

        public bool Add(News news)
        {
            return _repository.Add(news);
        }

        public bool Update(News news)
        {
            return _repository.Update(news);
        }

        public bool Delete(News news)
        {
            return _repository.Delete(news);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
