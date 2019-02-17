using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface IEmailTemplateService
    {
    }

    #endregion

    #region Class

    public class EmailTemplateService : GenericService<EmailTemplate>, IEmailTemplateService
    {
        public EmailTemplateService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion

}