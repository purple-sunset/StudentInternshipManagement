using System.IO;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StudentInternshipManagement.Services.Implements;

namespace StudentInternshipManagement.Web.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class ProfileController : TeacherBaseController
    {
        private readonly ITeacherService _teacherService;

        // GET: Student/Profile
        public ProfileController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public ActionResult Index()
        {
            var teacher = _teacherService.GetById(CurrentTeacherId);
            ViewBag.Email = CurrentUser.Email;
            return View(teacher);
        }

        public ActionResult Edit()
        {
            var teacher = _teacherService.GetById(CurrentTeacherId);
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, global::StudentInternshipManagement.Models.Entities.Teacher model)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Images/avatars/"),
                        $"{model.User.UserName}{extension}");
                    file.SaveAs(physicalPath);
                    model.User.Avatar = $"{model.User.UserName}{extension}";
                }

                ViewBag.Message = _teacherService.Update(model) ? "Thành công" : "Thất bại";
            }

            return RedirectToAction("Index");
        }
    }
}