using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Services;

namespace StudentInternshipManagement.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class ProfileController : Controller
    {
        private readonly TeacherService _service = new TeacherService();

        // GET: Student/Profile
        public ActionResult Index()
        {
            var id = User.Identity.GetUserName();
            var teacher = _service.GetById(id);
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ViewBag.Email = userManager.FindByName(id).Email;
            return View(teacher);
        }

        public ActionResult Edit()
        {
            var id = User.Identity.GetUserName();
            var teacher = _service.GetById(id);
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, global::Models.Teacher model)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Images/avatars/"),
                        $"{model.TeacherId}{extension}");
                    file.SaveAs(physicalPath);
                    model.Avatar = $"{model.TeacherId}{extension}";
                }

                ViewBag.Message = _service.Update(model) ? "Thành công" : "Thất bại";
            }

            return RedirectToAction("Index");
        }
    }
}