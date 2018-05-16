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
    public class CompanyRepository:IDisposable
    {
        private readonly WebContext _context=new WebContext();

        public IQueryable<Company> GetAll()
        {
            return _context.Companies;
        }
        public Company GetById(int id)
        {
            try
            {
                return _context.Companies.FirstOrDefault(s => s.CompanyId == id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool Add(Company company)
        {
            try
            {
                _context.Companies.Add(company);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(Company company)
        {
            try
            {
                _context.Entry(company).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(Company company)
        {
            var curr = GetById(company.CompanyId);
            if (curr == null)
                return false;

            try
            {
                _context.Companies.Remove(curr);
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
