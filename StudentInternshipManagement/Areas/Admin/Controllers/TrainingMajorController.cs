using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Models;
using Models.Entities;
using Services;
using Services.Implements;
using StudentInternshipManagement.Controllers;

namespace StudentInternshipManagement.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TrainingMajorController : BaseController
    {
        private readonly CompanyService _companyService;
        private readonly SubjectService _subjectService;
        private readonly TrainingMajorService _trainingMajorService;

        public TrainingMajorController(TrainingMajorService trainingMajorService, SubjectService subjectService,
            CompanyService companyService)
        {
            _trainingMajorService = trainingMajorService;
            _subjectService = subjectService;
            _companyService = companyService;
        }

        public ActionResult Index()
        {
            ViewBag.Subjects = _subjectService.GetAll();
            ViewBag.Companies = _companyService.GetAll();
            return View();
        }

        public ActionResult TrainingMajors_Read([DataSourceRequest] DataSourceRequest request)
        {
            var result = _trainingMajorService.GetAll().ToDataSourceResult(request, trainingMajor => new
            {
                TrainingMajorId = trainingMajor.Id,
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
                var entity = new TrainingMajor
                {
                    TrainingMajorName = trainingMajor.TrainingMajorName,
                    SubjectId = trainingMajor.SubjectId
                };

                _trainingMajorService.Add(entity);
            }

            return Json(new[] {trainingMajor}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TrainingMajors_Update([DataSourceRequest] DataSourceRequest request,
            TrainingMajor trainingMajor)
        {
            if (ModelState.IsValid)
            {
                var entity = new TrainingMajor
                {
                    Id = trainingMajor.Id,
                    TrainingMajorName = trainingMajor.TrainingMajorName,
                    SubjectId = trainingMajor.SubjectId
                };

                _trainingMajorService.Add(entity);
            }

            return Json(new[] {trainingMajor}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TrainingMajors_Destroy([DataSourceRequest] DataSourceRequest request,
            TrainingMajor trainingMajor)
        {
            if (ModelState.IsValid)
            {
                var entity = new TrainingMajor
                {
                    Id = trainingMajor.Id,
                    TrainingMajorName = trainingMajor.TrainingMajorName,
                    SubjectId = trainingMajor.SubjectId
                };

                _trainingMajorService.Delete(entity);
            }

            return Json(new[] {trainingMajor}.ToDataSourceResult(request, ModelState));
        }

        public ActionResult CompanyTrainingMajors_Read(int trainingMajorId,
            [DataSourceRequest] DataSourceRequest request)
        {
            var result = _trainingMajorService.GetAll().ToDataSourceResult(request, trainingMajor => new
            {
                TrainingMajorId = trainingMajor.Id,
                trainingMajor.TrainingMajorName,
                trainingMajor.SubjectId
            });

            return Json(result);
        }


        public ActionResult GetCompanyList(int majorId, [DataSourceRequest] DataSourceRequest request)
        {
            var result = _trainingMajorService.GetCompanyList(majorId).ToDataSourceResult(request, company => new
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