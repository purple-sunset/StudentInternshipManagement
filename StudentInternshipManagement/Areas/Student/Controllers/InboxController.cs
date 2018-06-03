using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentInternshipManagement.Areas.Student.Controllers
{
    public class InboxController : Controller
    {
        // GET: Student/Inbox
        public ActionResult Index()
        {
            return View();
        }
    }
}