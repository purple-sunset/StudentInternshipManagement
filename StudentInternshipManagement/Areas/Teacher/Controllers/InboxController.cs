using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Models;
using Models.Entities;
using PagedList;
using Services;
using Services.Implements;

namespace StudentInternshipManagement.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class InboxController : Controller
    {
        private readonly MessageService _service = new MessageService();
        private readonly GroupService _groupService = new GroupService();
        // GET: Student/Inbox
        public ActionResult Index()
        {
            var id = User.Identity.GetUserName();
            var messages = _service.GetByTeacher(id);
            ViewBag.UnRead = messages.Count(m => m.Type==1 && m.IsRead == false );
            return View();
        }

        public PartialViewResult GetMessagePage(int? page, int type)
        {
            var id = User.Identity.GetUserName();
            var messages = _service.GetByTeacher(id);
            var pageSize = 5;
            var mails = messages.Where(m => m.Type == type).ToPagedList(page ?? 1, pageSize);
            ViewBag.Type = type;
            return PartialView("_MessagePage", mails);
        }

        public ActionResult Write(string student)
        {
            var id = User.Identity.GetUserName();
            ViewBag.StudentList = _groupService.GetByTeacher(id).Select(g=>(g.Members.Select(s=>s.StudentId)));
            ViewBag.Student = student;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Write(HttpPostedFileBase file, Message model)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var filePath = Server.MapPath($"~/Attachment/{model.TeacherId}/");
                    Directory.CreateDirectory($"{filePath}");
                    var physicalPath = Path.Combine(filePath, $"{file.FileName}");
                    file.SaveAs(physicalPath);
                    model.File = $"{file.FileName}";
                }

                model.Type = 1;
                model.Time = DateTime.Now;

                ViewBag.Message = _service.Add(model) ? "Gửi thành công" : "Gửi thất bại";
            }

            return RedirectToAction("Index");
        }

        public ActionResult View(int? id)
        {
            var message = _service.GetById(id??1);
            return View(message);
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}