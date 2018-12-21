using Models.Entities;

namespace Services.Interfaces
{
    public interface IAdminService : IGenericService<Admin>
    {
        Admin GetByTeacherCode(string teacherCode);
    }
}