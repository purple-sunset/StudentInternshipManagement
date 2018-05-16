using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class CompanyService : IDisposable
    {
        private readonly CompanyRepository _repository=new CompanyRepository();

        public IQueryable<Company> GetAll()
        {
            return _repository.GetAll();
        }
        public Company GetById(int id)
        {
            return _repository.GetById(id);
        }

        public bool Add(Company company)
        {
            return _repository.Add(company);
        }

        public bool Update(Company company)
        {
            return _repository.Update(company);
        }

        public bool Delete(Company company)
        {
            return _repository.Delete(company);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
