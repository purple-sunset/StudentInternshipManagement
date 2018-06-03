using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class InternshipService : IDisposable
    {
        private readonly InternshipRepository _repository=new InternshipRepository();

        public IQueryable<Internship> GetAll()
        {
            return _repository.GetAll();
        }
        public IQueryable<Internship> GetBySemester(int semesterId)
        {
            return _repository.GetBySemester(semesterId);
        }

        public IQueryable<Internship> GetByLatestSemester()
        {
            using (var semesterRepository = new SemesterRepository())
            {
                var semesterId = semesterRepository.GetLatest().SemesterId;
                return _repository.GetBySemester(semesterId);
            }
        }
        public Internship GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Internship> GetByStudent(string studentId)
        {
            return _repository.GetByStudent(studentId);
        }

        public bool Add(Internship internship)
        {
            return _repository.Add(internship);
        }

        public bool Update(Internship internship)
        {
            return _repository.Update(internship);
        }

        public bool Delete(Internship internship)
        {
            return _repository.Delete(internship);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
