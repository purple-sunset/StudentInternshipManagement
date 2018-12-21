using Models.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class NewsService : GenericService<News>, INewsService
    {
        public NewsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}