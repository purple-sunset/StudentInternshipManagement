using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using StudentInternshipManagement.Services.Implements;

namespace StudentInternshipManagement.Web.Areas.Student.Controllers
{
    [Authorize (Roles="Student")]
    public class CompanyController : StudentBaseController
    {
        private readonly ICompanyService _companyService;
        private readonly ITrainingMajorService _trainingMajorService;

        public CompanyController(ICompanyService companyService, ITrainingMajorService trainingMajorService)
        {
            _companyService = companyService;
            _trainingMajorService = trainingMajorService;
        }

        public ActionResult Index()
        {
            ViewBag.TrainingMajors = _trainingMajorService.GetAll();
            return null;
        }

        public ActionResult Companies_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _companyService.GetAll().ToDataSourceResult(request, company => new
            {
                company.Id,
                company.CompanyName,
                company.CompanyDescription,
                company.Address,
                company.Email,
                company.Phone
            });

            return Json(result);
        }
    }
}
