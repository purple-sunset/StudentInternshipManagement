using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using StudentInternshipManagement.Models.Constants;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Services.Implements;

namespace StudentInternshipManagement.Web.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class InboxController : TeacherBaseController
    {
        private readonly IGroupService _groupService;
        private readonly IMessageService _messageService;
        // GET: Student/Inbox
        public InboxController(IGroupService groupService, IMessageService messageService)
        {
            _groupService = groupService;
            _messageService = messageService;
        }

        public ActionResult Index()
        {
            var id = User.Identity.GetUserName();
            var messages = _messageService.GetReceivedEmail(id);
            ViewBag.UnRead = messages.Count(m => m.Status != MessageStatus.Read);
            return View();
        }

        public PartialViewResult GetMessagePage(int? page, int type)
        {
            var id = User.Identity.GetUserName();
            IQueryable<Message> messages = null;
            var pageSize = 5;
            switch (type)
            {
                case 1:
                    messages = _messageService.GetReceivedEmail(id);
                    break;
                case 2:
                    messages = _messageService.GetSentEmail(id);
                    break;
                case 3:
                    messages = _messageService.GetDraftEmail(id);
                    break;
            }

            var mails = messages.ToPagedList(page ?? 1, pageSize);
            ViewBag.Type = type;
            return PartialView("_MessagePage", mails);
        }

        public ActionResult Write(string student)
        {
            ViewBag.StudentList = _groupService.GetByTeacher(CurrentTeacherId).Select(g=>(g.Members.Select(s=>s.User.FullName)));
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
                    var filePath = Server.MapPath($"~/Attachment/{model.SenderId}/");
                    Directory.CreateDirectory($"{filePath}");
                    var physicalPath = Path.Combine(filePath, $"{file.FileName}");
                    file.SaveAs(physicalPath);
                    model.File = $"{file.FileName}";
                }

                ViewBag.Message = _messageService.Add(model) ? "Gửi thành công" : "Gửi thất bại";
            }

            return RedirectToAction("Index");
        }

        public ActionResult View(int? id)
        {
            var message = _messageService.GetById(id??1);
            return View(message);
        }
    }
}