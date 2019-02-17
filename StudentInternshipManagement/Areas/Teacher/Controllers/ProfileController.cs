using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Services;
using Services.Implements;
using Services.Interfaces;

namespace StudentInternshipManagement.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class ProfileController : Controller
    {
        private readonly ITeacherService _teacherService;

        // GET: Student/Profile
        public ProfileController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public ActionResult Index()
        {
            var id = User.Identity.GetUserName();
            var teacher = _teacherService.GetByTeacherCode(id);
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ViewBag.Email = userManager.FindByName(id).Email;
            return View(teacher);
        }

        public ActionResult Edit()
        {
            var id = User.Identity.GetUserName();
            var teacher = _teacherService.GetByTeacherCode(id);
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, global::Models.Entities.Teacher model)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Images/avatars/"),
                        $"{model.TeacherCode}{extension}");
                    file.SaveAs(physicalPath);
                    model.User.Avatar = $"{model.TeacherCode}{extension}";
                }

                ViewBag.Message = _teacherService.Update(model) ? "Thành công" : "Thất bại";
            }

            return RedirectToAction("Index");
        }
    }
}