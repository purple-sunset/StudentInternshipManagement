﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Models;
 using Models.Entities;
 using Services;
 using Services.Implements;

namespace StudentInternshipManagement.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        private readonly NewsService _service = new NewsService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Newses_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _service.GetAll().ToDataSourceResult(request, news => new {
                NewsId = news.NewsId,
                Title = news.Title,
                Content = news.Content,
                Time = news.Time
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Newses_Create([DataSourceRequest]DataSourceRequest request, News news)
        {
            if (ModelState.IsValid)
            {
                var entity = new News
                {
                    Title = news.Title,
                    Content = news.Content,
                    Time = news.Time
                };

                _service.Add(entity);
            }

            return Json(new[] { news }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Newses_Update([DataSourceRequest]DataSourceRequest request, News news)
        {
            if (ModelState.IsValid)
            {
                var entity = new News
                {
                    NewsId = news.NewsId,
                    Title = news.Title,
                    Content = news.Content,
                    Time = news.Time
                };

                _service.Update(entity);
            }

            return Json(new[] { news }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Newses_Destroy([DataSourceRequest]DataSourceRequest request, News news)
        {
            if (ModelState.IsValid)
            {
                var entity = new News
                {
                    NewsId = news.NewsId,
                    Title = news.Title,
                    Content = news.Content,
                    Time = news.Time
                };

                _service.Delete(entity);
            }

            return Json(new[] { news }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}
