using Models.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class EmailTemplateService : GenericService<EmailTemplate>, IEmailTemplateService
    {
        public EmailTemplateService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}