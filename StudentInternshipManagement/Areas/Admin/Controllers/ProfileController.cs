using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Services;

namespace StudentInternshipManagement.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProfileController : Controller
    {
        private readonly AdminService _service = new AdminService();

        // GET: Student/Profile
        public ActionResult Index()
        {
            var id = User.Identity.GetUserName();
            var admin = _service.GetById(id);
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ViewBag.Email = userManager.FindByName(id).Email;
            return View(admin);
        }

        public ActionResult Edit()
        {
            var id = User.Identity.GetUserName();
            var admin = _service.GetById(id);
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, global::Models.Admin model)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Images/avatars/"),
                        $"{model.AdminId}{extension}");
                    file.SaveAs(physicalPath);
                    model.Avatar = $"{model.AdminId}{extension}";
                }

                ViewBag.Message = _service.Update(model) ? "Thành công" : "Thất bại";
            }

            return RedirectToAction("Index");
        }
    }
}