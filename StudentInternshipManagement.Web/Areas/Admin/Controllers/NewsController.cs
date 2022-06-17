using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Services.Implements;
using StudentInternshipManagement.Web.Controllers;

namespace StudentInternshipManagement.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsController : AdminBaseController
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List");
        }

        public ActionResult Newses_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = _newsService.GetAll().ToDataSourceResult(request, news => new
            {
                news.Id,
                news.Title,
                news.Content,
                news.Time
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Newses_Create([DataSourceRequest] DataSourceRequest request, News news)
        {
            if (ModelState.IsValid)
            {
                _newsService.Add(news);
            }

            return Json(new[] {news}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Newses_Update([DataSourceRequest] DataSourceRequest request, News news)
        {
            if (ModelState.IsValid)
            {
                _newsService.Update(news);
            }

            return Json(new[] {news}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Newses_Destroy([DataSourceRequest] DataSourceRequest request, News news)
        {
            if (ModelState.IsValid)
            {
                _newsService.Delete(news);
            }

            return Json(new[] {news}.ToDataSourceResult(request, ModelState));
        }
    }
}