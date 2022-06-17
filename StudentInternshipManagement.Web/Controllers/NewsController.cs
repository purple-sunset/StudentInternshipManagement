﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Services.Implements;

namespace StudentInternshipManagement.Web.Controllers
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

        public ActionResult List()
        {
            return PartialView("_List");
        }

        public async Task<ActionResult> News_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<News> products = _service.GetAll();
            DataSourceResult result = await products.ToDataSourceResultAsync(request);
            return Json(result);
        }

        // GET: News
        public async Task<ActionResult> View(int id)
        {
            News news = await _service.GetByIdAsync(id);
            return View(news);
        }
    }
}