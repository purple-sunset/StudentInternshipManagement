using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Models.Entities;
using Services.Interfaces;
using StudentInternshipManagement.Controllers;

namespace StudentInternshipManagement.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TrainingMajorController : BaseController
    {
        private readonly ICompanyService _companyService;
        private readonly ISubjectService _subjectService;
        private readonly ITrainingMajorService _trainingMajorService;

        public TrainingMajorController(ICompanyService companyService, ISubjectService subjectService,
            ITrainingMajorService trainingMajorService)
        {
            _companyService = companyService;
            _subjectService = subjectService;
            _trainingMajorService = trainingMajorService;
        }

        public ActionResult Index()
        {
            ViewBag.Subjects = _subjectService.GetAll();
            ViewBag.Companies = _companyService.GetAll();
            return View();
        }

        public ActionResult TrainingMajors_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = _trainingMajorService.GetAll().ToDataSourceResult(request, trainingMajor => new
            {
                trainingMajor.Id,
                trainingMajor.TrainingMajorName,
                trainingMajor.SubjectId
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TrainingMajors_Create([DataSourceRequest] DataSourceRequest request,
            TrainingMajor trainingMajor)
        {
            if (ModelState.IsValid)
            {
                _trainingMajorService.Add(trainingMajor);
            }

            return Json(new[] {trainingMajor}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TrainingMajors_Update([DataSourceRequest] DataSourceRequest request,
            TrainingMajor trainingMajor)
        {
            if (ModelState.IsValid)
            {
                _trainingMajorService.Add(trainingMajor);
            }

            return Json(new[] {trainingMajor}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TrainingMajors_Destroy([DataSourceRequest] DataSourceRequest request,
            TrainingMajor trainingMajor)
        {
            if (ModelState.IsValid)
            {
                _trainingMajorService.Delete(trainingMajor);
            }

            return Json(new[] {trainingMajor}.ToDataSourceResult(request, ModelState));
        }

        public ActionResult CompanyTrainingMajors_Read(int trainingMajorId,
            [DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = _trainingMajorService.GetAll().ToDataSourceResult(request, trainingMajor => new
            {
                TrainingMajorId = trainingMajor.Id,
                trainingMajor.TrainingMajorName,
                trainingMajor.SubjectId
            });

            return Json(result);
        }


        public ActionResult GetCompanyList(int majorId, [DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = _trainingMajorService.GetCompanyList(majorId).ToDataSourceResult(request,
                company => new
                {
                    CompanyId = company.Id,
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