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
 using Services;

namespace StudentInternshipManagement.Areas.Admin.Controllers
{
    public class SemesterController : Controller
    {
        private SemesterService _service=new SemesterService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Semesters_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _service.GetAll().ToDataSourceResult(request, semester => new {
                SemesterId = semester.SemesterId,
                StartDate = semester.StartDate,
                EndDate = semester.EndDate
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Semesters_Create([DataSourceRequest]DataSourceRequest request, Semester semester)
        {
            if (ModelState.IsValid)
            {
                var entity = new Semester
                {
                    SemesterId = semester.SemesterId,
                    StartDate = semester.StartDate,
                    EndDate = semester.EndDate
                };

                _service.Add(entity);
            }

            return Json(new[] { semester }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Semesters_Update([DataSourceRequest]DataSourceRequest request, Semester semester)
        {
            if (ModelState.IsValid)
            {
                var entity = new Semester
                {
                    SemesterId = semester.SemesterId,
                    StartDate = semester.StartDate,
                    EndDate = semester.EndDate
                };

                _service.Update(entity);
            }

            return Json(new[] { semester }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Semesters_Destroy([DataSourceRequest]DataSourceRequest request, Semester semester)
        {
            if (ModelState.IsValid)
            {
                var entity = new Semester
                {
                    SemesterId = semester.SemesterId,
                    StartDate = semester.StartDate,
                    EndDate = semester.EndDate
                };

                _service.Delete(entity);
            }

            return Json(new[] { semester }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}
