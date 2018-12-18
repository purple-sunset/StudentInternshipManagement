using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Models;
using Models.Entities;
using Services;
using Services.Implements;
using Services.Interfaces;

namespace StudentInternshipManagement.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CompanyTrainingMajorController : BaseController
    {
        private readonly ICompanyService _companyService;
        private readonly CompanyTrainingMajorService _companyTrainingMajorService;
        private readonly TrainingMajorService _trainingMajorService;

        public CompanyTrainingMajorController(CompanyTrainingMajorService companyTrainingMajorService,
            TrainingMajorService trainingMajorService, ICompanyService companyService)
        {
            _companyTrainingMajorService = companyTrainingMajorService;
            _trainingMajorService = trainingMajorService;
            _companyService = companyService;
        }

        public ActionResult Index()
        {
            ViewBag.Companies = _companyService.GetAll();
            ViewBag.TrainingMajors = _trainingMajorService.GetAll();
            return View();
        }

        public ActionResult CompanyTrainingMajors_Read([DataSourceRequest] DataSourceRequest request, int? companyId,
            int? majorId)
        {
            IQueryable<CompanyTrainingMajor> datasource;

            if (companyId != null)
                datasource = _companyTrainingMajorService.GetByCompany(companyId.Value);
            else if (majorId != null)
                datasource = _companyTrainingMajorService.GetByTrainingMajor(majorId.Value);
            else
                datasource = _companyTrainingMajorService.GetAll();

            var result = datasource.ToDataSourceResult(request, companyTrainingMajor => new
            {
                companyTrainingMajor.CompanyId,
                companyTrainingMajor.TrainingMajorId,
                companyTrainingMajor.TotalTraineeCount,
                companyTrainingMajor.AvailableTraineeCount
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CompanyTrainingMajors_Create([DataSourceRequest] DataSourceRequest request,
            CompanyTrainingMajor companyTrainingMajor)
        {
            if (ModelState.IsValid)
            {
                var entity = new CompanyTrainingMajor
                {
                    CompanyId = companyTrainingMajor.CompanyId,
                    TrainingMajorId = companyTrainingMajor.TrainingMajorId,
                    TotalTraineeCount = companyTrainingMajor.TotalTraineeCount,
                    AvailableTraineeCount = companyTrainingMajor.AvailableTraineeCount
                };

                _companyTrainingMajorService.Add(entity);
            }

            return Json(new[] {companyTrainingMajor}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CompanyTrainingMajors_Update([DataSourceRequest] DataSourceRequest request,
            CompanyTrainingMajor companyTrainingMajor)
        {
            if (ModelState.IsValid)
            {
                var entity = new CompanyTrainingMajor
                {
                    CompanyId = companyTrainingMajor.CompanyId,
                    TrainingMajorId = companyTrainingMajor.TrainingMajorId,
                    TotalTraineeCount = companyTrainingMajor.TotalTraineeCount,
                    AvailableTraineeCount = companyTrainingMajor.AvailableTraineeCount
                };

                _companyTrainingMajorService.Update(entity);
            }

            return Json(new[] {companyTrainingMajor}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CompanyTrainingMajors_Destroy([DataSourceRequest] DataSourceRequest request,
            CompanyTrainingMajor companyTrainingMajor)
        {
            if (ModelState.IsValid)
            {
                var entity = new CompanyTrainingMajor
                {
                    CompanyId = companyTrainingMajor.CompanyId,
                    TrainingMajorId = companyTrainingMajor.TrainingMajorId,
                    TotalTraineeCount = companyTrainingMajor.TotalTraineeCount,
                    AvailableTraineeCount = companyTrainingMajor.AvailableTraineeCount
                };

                _companyTrainingMajorService.Delete(entity);
            }

            return Json(new[] {companyTrainingMajor}.ToDataSourceResult(request, ModelState));
        }
    }
}