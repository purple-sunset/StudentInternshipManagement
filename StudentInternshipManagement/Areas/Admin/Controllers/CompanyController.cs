using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Models;
using Models.Entities;
using Services;
using Services.Implements;
using Services.Interfaces;
using StudentInternshipManagement.Controllers;

namespace StudentInternshipManagement.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;
        private readonly TrainingMajorService _trainingMajorService;

        public CompanyController(ICompanyService service, TrainingMajorService trainingMajorService)
        {
            _companyService = service;
            _trainingMajorService = trainingMajorService;
        }

        public ActionResult Index()
        {
            ViewBag.TrainingMajors = _trainingMajorService.GetAll();
            return View();
        }

        public ActionResult Companies_Read([DataSourceRequest] DataSourceRequest request)
        {
            var result = _companyService.GetAll().ToDataSourceResult(request, company => new
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Companies_Create([DataSourceRequest] DataSourceRequest request, Company company)
        {
            if (ModelState.IsValid)
            {
                var entity = new Company
                {
                    CompanyName = company.CompanyName,
                    CompanyDescription = company.CompanyDescription,
                    Address = company.Address,
                    Email = company.Email,
                    Phone = company.Phone
                };

                _companyService.Add(entity);
            }

            return Json(new[] {company}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Companies_Update([DataSourceRequest] DataSourceRequest request, Company company)
        {
            if (ModelState.IsValid)
            {
                var entity = new Company
                {
                    Id = company.Id,
                    CompanyName = company.CompanyName,
                    CompanyDescription = company.CompanyDescription,
                    Address = company.Address,
                    Email = company.Email,
                    Phone = company.Phone
                };

                _companyService.Update(entity);
            }

            return Json(new[] {company}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Companies_Destroy([DataSourceRequest] DataSourceRequest request, Company company)
        {
            if (ModelState.IsValid)
            {
                var entity = new Company
                {
                    Id = company.Id,
                    CompanyName = company.CompanyName,
                    CompanyDescription = company.CompanyDescription,
                    Address = company.Address,
                    Email = company.Email,
                    Phone = company.Phone
                };

                _companyService.Delete(entity);
            }

            return Json(new[] {company}.ToDataSourceResult(request, ModelState));
        }
    }
}