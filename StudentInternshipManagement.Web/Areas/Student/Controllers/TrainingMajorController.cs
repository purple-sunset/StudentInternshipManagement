using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using StudentInternshipManagement.Services.Implements;

namespace StudentInternshipManagement.Web.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class TrainingMajorController : StudentBaseController
    {
        private readonly ITrainingMajorService _trainingMajorService;
        private readonly ISubjectService _subjectService;
        private readonly ICompanyService _companyService;

        public TrainingMajorController(ITrainingMajorService trainingMajorService, ISubjectService subjectService, ICompanyService companyService)
        {
            _trainingMajorService = trainingMajorService;
            _subjectService = subjectService;
            _companyService = companyService;
        }

        public ActionResult Index()
        {
            ViewBag.Subjects = _subjectService.GetAll();
            ViewBag.Companies = _companyService.GetAll();
            return null;
        }

        public ActionResult TrainingMajors_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _trainingMajorService.GetAll().ToDataSourceResult(request, trainingMajor => new {
                trainingMajor.Id,
                trainingMajor.TrainingMajorName,
                trainingMajor.SubjectId
            });

            return Json(result);
        }

        public ActionResult GetCompanyList(int majorId, [DataSourceRequest]DataSourceRequest request)
        {
            return Json(_trainingMajorService.GetCompanyList(majorId), JsonRequestBehavior.AllowGet);
        }
    }
}
