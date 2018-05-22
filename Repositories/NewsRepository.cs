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
    public class NewsRepository:IDisposable
    {
        private readonly WebContext _context = new WebContext();

        public IQueryable<News> GetAll()
        {
            try
            {
                return _context.Newses;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }
        public News GetById(int id)
        {
            try
            {
                return _context.Newses.FirstOrDefault(s => s.NewsId == id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool Add(News news)
        {
            try
            {
                _context.Newses.Add(news);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(News news)
        {
            try
            {
                _context.Entry(news).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(News news)
        {
            var curr = GetById(news.NewsId);
            if (curr == null)
                return false;

            try
            {
                _context.Newses.Remove(curr);
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
