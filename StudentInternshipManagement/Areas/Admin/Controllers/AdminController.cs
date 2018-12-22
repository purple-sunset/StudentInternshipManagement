using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Services.Interfaces;
using StudentInternshipManagement.Controllers;

namespace StudentInternshipManagement.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;
        private readonly IDepartmentService _departmentService;

        public AdminController(IAdminService adminService, IDepartmentService departmentService)
        {
            _adminService = adminService;
            _departmentService = departmentService;
        }

        public ActionResult Index()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }

        public ActionResult Admins_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = _adminService.GetAll().ToDataSourceResult(request, admin => new
            {
                admin.Id,
                admin.AdminCode,
                admin.AdminName,
                admin.BirthDate,
                admin.Address,
                admin.Phone,
                admin.DepartmentId
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Admins_Create([DataSourceRequest] DataSourceRequest request,
            global::Models.Entities.Admin admin)
        {
            if (ModelState.IsValid)
            {
                _adminService.Add(admin);
            }

            return Json(new[] {admin}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Admins_Update([DataSourceRequest] DataSourceRequest request,
            global::Models.Entities.Admin admin)
        {
            if (ModelState.IsValid)
            {
                _adminService.Update(admin);
            }

            return Json(new[] {admin}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Admins_Destroy([DataSourceRequest] DataSourceRequest request,
            global::Models.Entities.Admin admin)
        {
            if (ModelState.IsValid)
            {
                _adminService.Delete(admin);
            }

            return Json(new[] {admin}.ToDataSourceResult(request, ModelState));
        }
    }
}