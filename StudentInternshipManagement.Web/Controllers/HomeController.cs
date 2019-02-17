using System.Web.Mvc;

namespace StudentInternshipManagement.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}