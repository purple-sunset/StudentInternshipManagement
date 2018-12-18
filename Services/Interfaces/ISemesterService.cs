using Models;
using Models.Entities;

namespace Services.Interfaces
{
    public interface ISemesterService:IGenericService<Semester>
    {
        Semester GetLatest();
    }
}