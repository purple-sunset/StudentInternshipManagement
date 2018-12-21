using System.Web.Mvc;

namespace StudentInternshipManagement.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}