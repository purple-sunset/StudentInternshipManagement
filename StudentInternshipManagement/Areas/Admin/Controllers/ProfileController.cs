using System.IO;
using System.Web;
using System.Web.Mvc;
using Services.Interfaces;
using StudentInternshipManagement.Controllers;

namespace StudentInternshipManagement.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProfileController : BaseController
    {
        private readonly IAdminService _adminService;

        public ProfileController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // GET: Student/Profile
        public ActionResult Index()
        {
            global::Models.Entities.Admin admin = _adminService.GetByAdminCode(CurrentUser.Id);
            ViewBag.Email = CurrentUser.Email;
            return View(admin);
        }

        public ActionResult Edit()
        {
            global::Models.Entities.Admin admin = _adminService.GetByAdminCode(CurrentUser.Id);
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, global::Models.Entities.Admin model)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string extension = Path.GetExtension(file.FileName);
                    string physicalPath = Path.Combine(Server.MapPath("~/Images/avatars/"),
                        $"{model.AdminCode}{extension}");
                    file.SaveAs(physicalPath);
                    CurrentUser.Avatar = $"{model.AdminCode}{extension}";
                }

                ViewBag.Message = _adminService.Update(model) ? "Thành công" : "Thất bại";
            }

            return RedirectToAction("Index");
        }
    }
}