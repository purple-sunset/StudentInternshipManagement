using System;
using System.Linq;
using Models;
using Models.Entities;
using Repositories;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class LearningClassService : GenericService<LearningClass>, ILearningClassService
    {
        private readonly ILearningClassStudentService _learningClassStudentService;
        public LearningClassService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public LearningClassService(IUnitOfWork unitOfWork, ILearningClassStudentService learningClassStudentService) : base(unitOfWork)
        {
            _learningClassStudentService = learningClassStudentService;
        }


        public IQueryable<Student> GetStudentList(int classId)
        {
            return _learningClassStudentService.GetByClass(classId).Select(m => m.Student);
        }
    }
}
