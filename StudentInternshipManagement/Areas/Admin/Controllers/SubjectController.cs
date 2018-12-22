using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Models.Entities;
using Services.Interfaces;
using StudentInternshipManagement.Controllers;

namespace StudentInternshipManagement.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubjectController : BaseController
    {
        private readonly IDepartmentService _departmentService;
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService, IDepartmentService departmentService)
        {
            _subjectService = subjectService;
            _departmentService = departmentService;
        }

        public ActionResult Index()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }

        public ActionResult Subjects_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = _subjectService.GetAll().ToDataSourceResult(request, subject => new
            {
                subject.Id,
                subject.SubjectName,
                subject.DepartmentId
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Subjects_Create([DataSourceRequest] DataSourceRequest request, Subject subject)
        {
            if (ModelState.IsValid)
            {
                _subjectService.Add(subject);
            }

            return Json(new[] {subject}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Subjects_Update([DataSourceRequest] DataSourceRequest request, Subject subject)
        {
            if (ModelState.IsValid)
            {
                _subjectService.Update(subject);
            }

            return Json(new[] {subject}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Subjects_Destroy([DataSourceRequest] DataSourceRequest request, Subject subject)
        {
            if (ModelState.IsValid)
            {
                _subjectService.Delete(subject);
            }

            return Json(new[] {subject}.ToDataSourceResult(request, ModelState));
        }
    }
}