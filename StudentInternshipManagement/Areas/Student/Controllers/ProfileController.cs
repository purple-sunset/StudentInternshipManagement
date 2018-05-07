using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentInternshipManagement.Areas.Student.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Student/Profile
        public ActionResult Index()
        {
            return View();
        }
    }
}