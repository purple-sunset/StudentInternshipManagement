using System;
using System.Linq;
using Models;
using Models.Entities;
using Repositories;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class TeacherService : GenericService<Teacher>, ITeacherService
    {
        public TeacherService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
