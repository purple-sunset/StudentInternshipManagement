using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Services;
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
            var admin = _adminService.GetByTeacherCode(CurrentUser.Id);
            ViewBag.Email = CurrentUser.Email;
            return View(admin);
        }

        public ActionResult Edit()
        {
            var admin = _adminService.GetByTeacherCode(CurrentUser.Id);
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
                    var extension = Path.GetExtension(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Images/avatars/"),
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