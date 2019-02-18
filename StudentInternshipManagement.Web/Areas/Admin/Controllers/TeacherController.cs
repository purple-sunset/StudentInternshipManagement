using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using StudentInternshipManagement.Services.Implements;
using StudentInternshipManagement.Web.Controllers;

namespace StudentInternshipManagement.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeacherController : AdminBaseController
    {
        private readonly IDepartmentService _departmentService;
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService, IDepartmentService departmentService)
        {
            _teacherService = teacherService;
            _departmentService = departmentService;
        }

        public ActionResult Index()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }

        public ActionResult Teachers_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = _teacherService.GetAll().ToDataSourceResult(request);

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Teachers_Create([DataSourceRequest] DataSourceRequest request,
            global::StudentInternshipManagement.Models.Entities.Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _teacherService.Add(teacher);
            }

            return Json(new[] {teacher}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Teachers_Update([DataSourceRequest] DataSourceRequest request,
            global::StudentInternshipManagement.Models.Entities.Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _teacherService.Update(teacher);
            }

            return Json(new[] {teacher}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Teachers_Destroy([DataSourceRequest] DataSourceRequest request,
            global::StudentInternshipManagement.Models.Entities.Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _teacherService.Delete(teacher);
            }

            return Json(new[] {teacher}.ToDataSourceResult(request, ModelState));
        }
    }
}