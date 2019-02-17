using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Services.Implements;
using StudentInternshipManagement.Web.Controllers;

namespace StudentInternshipManagement.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartmentController : AdminBaseController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Departments_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = _departmentService.GetAll().ToDataSourceResult(request, department => new
            {
                department.Id,
                department.DepartmentName
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Departments_Create([DataSourceRequest] DataSourceRequest request, Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentService.Add(department);
            }

            return Json(new[] {department}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Departments_Update([DataSourceRequest] DataSourceRequest request, Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentService.Update(department);
            }

            return Json(new[] {department}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Departments_Destroy([DataSourceRequest] DataSourceRequest request, Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentService.Delete(department);
            }

            return Json(new[] {department}.ToDataSourceResult(request, ModelState));
        }
    }
}