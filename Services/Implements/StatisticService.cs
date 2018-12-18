using System;
using System.Linq;
using Models.Entities;
using Repositories;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class StatisticService : GenericService<Statistic>, IStatisticService
    {
        public StatisticService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
