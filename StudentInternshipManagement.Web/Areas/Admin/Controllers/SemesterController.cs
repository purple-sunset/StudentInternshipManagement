using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Services.Implements;
using StudentInternshipManagement.Web.Controllers;

namespace StudentInternshipManagement.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SemesterController : AdminBaseController
    {
        private readonly ISemesterService _semesterService;

        public SemesterController(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Semesters_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = _semesterService.GetAll().ToDataSourceResult(request, semester => new
            {
                semester.Id,
                semester.StartDate,
                semester.EndDate
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Semesters_Create([DataSourceRequest] DataSourceRequest request, Semester semester)
        {
            if (ModelState.IsValid)
            {
                _semesterService.Add(semester);
            }

            return Json(new[] {semester}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Semesters_Update([DataSourceRequest] DataSourceRequest request, Semester semester)
        {
            if (ModelState.IsValid)
            {
                _semesterService.Update(semester);
            }

            return Json(new[] {semester}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Semesters_Destroy([DataSourceRequest] DataSourceRequest request, Semester semester)
        {
            if (ModelState.IsValid)
            {
                _semesterService.Delete(semester);
            }

            return Json(new[] {semester}.ToDataSourceResult(request, ModelState));
        }
    }
}