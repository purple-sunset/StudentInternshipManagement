using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Services.Implements;
using Services.Interfaces;

namespace StudentInternshipManagement.Controllers
{
    public class NewsController : BaseController
    {
        private readonly INewsService _service;

        public NewsController(INewsService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult News_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_service.GetAll().ToDataSourceResult(request));
        }

        // GET: News
        public ActionResult View(int id)
        {
            var news = _service.GetById(id);
            return View(news);
        }
    }
}