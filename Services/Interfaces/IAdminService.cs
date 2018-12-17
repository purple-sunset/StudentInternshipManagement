using Models;

namespace Services.Interfaces
{
    public interface IAdminService: IGenericService<Admin>
    {
        Admin GetByTeacherCode(string code);
    }
}