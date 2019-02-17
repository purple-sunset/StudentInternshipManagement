using Models.Entities;

namespace Services.Interfaces
{
    public interface ITeacherService : IGenericService<Teacher>
    {
        Teacher GetByTeacherCode(string teacherCode);
    }
}