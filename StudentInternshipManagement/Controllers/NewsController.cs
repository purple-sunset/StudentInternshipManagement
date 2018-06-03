using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Services;

namespace StudentInternshipManagement.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsService _service = new NewsService();
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
            var news =_service.GetById(id);
            return View(news);
        }
    }
}