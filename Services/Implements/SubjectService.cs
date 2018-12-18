using Models.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class SubjectService : GenericService<Subject>, ISubjectService
    {
        public SubjectService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
