using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Models;
using Services;
namespace StudentInternshipManagement.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class ProfileController : Controller
    {
        private readonly StudentService _service = new StudentService();
        // GET: Student/Profile
        public ActionResult Index()
        {
            var id = User.Identity.GetUserName();
            var student = _service.GetById(id);
            return View(student);
        }

        public ActionResult Edit()
        {
            var id = User.Identity.GetUserName();
            var student = _service.GetById(id);
            return View(student);
        }

        public ActionResult Edit(IEnumerable<HttpPostedFileBase> files, global::Models.Student model)
        {
            if (ModelState.IsValid)
            {
                var file = files.FirstOrDefault();
                file?.SaveAs(@"~/Images/Avatar/" + model.StudentId);


            }

            return RedirectToAction("Index");
        }
    }
}