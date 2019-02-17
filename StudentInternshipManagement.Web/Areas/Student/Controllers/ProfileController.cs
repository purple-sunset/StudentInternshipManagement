using System.IO;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StudentInternshipManagement.Services.Implements;

namespace StudentInternshipManagement.Web.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class ProfileController : StudentBaseController
    {
        private readonly IStudentService _studentService;

        public ProfileController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: Student/Profile
        public ActionResult Index()
        {
            var student = _studentService.GetById(CurrentStudentId);
            ViewBag.Email = CurrentUser.Email;
            return View(student);
        }

        public ActionResult Edit()
        {
            var student = _studentService.GetById(CurrentStudentId);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, global::StudentInternshipManagement.Models.Entities.Student model)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Images/avatars/"), $"{model.User.Avatar}{extension}");
                    file.SaveAs(physicalPath);
                    model.User.Avatar = $"{model.User.UserName}{extension}";
                }

                ViewBag.Message = _studentService.Update(model) ? "Thành công" : "Thất bại";
            }

            return RedirectToAction("Index");
        }
    }
}