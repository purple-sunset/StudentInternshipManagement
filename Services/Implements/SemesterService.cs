using System.Linq;
using Models;
using Models.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class SemesterService : GenericService<Semester>, ISemesterService
    {
        public SemesterService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Semester GetLatest()
        {
            return UnitOfWork.Repository<Semester>().TableNoTracking.OrderByDescending(s => s.Id).FirstOrDefault();
        }
    }
}
