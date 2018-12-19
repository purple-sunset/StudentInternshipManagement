using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Semester> GetLatestAsync()
        {
            return await UnitOfWork.Repository<Semester>().TableNoTracking.OrderByDescending(s => s.Id).FirstOrDefaultAsync();
        }
    }
}
