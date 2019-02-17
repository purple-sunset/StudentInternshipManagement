using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Services.Implements;
using StudentInternshipManagement.Web.Controllers;

namespace StudentInternshipManagement.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CompanyController : AdminBaseController
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
            return View();
        }

        public ActionResult Companies_Read([DataSourceRequest] DataSourceRequest request)
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Companies_Create([DataSourceRequest] DataSourceRequest request, Company company)
        {
            if (ModelState.IsValid)
            {
                _companyService.Add(company);
            }

            return Json(new[] {company}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Companies_Update([DataSourceRequest] DataSourceRequest request, Company company)
        {
            if (ModelState.IsValid)
            {
                _companyService.Update(company);
            }

            return Json(new[] {company}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Companies_Destroy([DataSourceRequest] DataSourceRequest request, Company company)
        {
            if (ModelState.IsValid)
            {
                _companyService.Delete(company);
            }

            return Json(new[] {company}.ToDataSourceResult(request, ModelState));
        }
    }
}