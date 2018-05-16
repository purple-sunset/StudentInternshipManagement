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
    public class SubjectController : Controller
    {
        private readonly SubjectService _service = new SubjectService();
        private readonly DepartmentService _departmentService = new DepartmentService();

        public ActionResult Index()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }

        public ActionResult Subjects_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _service.GetAll().ToDataSourceResult(request, subject => new {
                SubjectId = subject.SubjectId,
                SubjectName = subject.SubjectName,
                DepartmentId = subject.DepartmentId
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Subjects_Create([DataSourceRequest]DataSourceRequest request, Subject subject)
        {
            if (ModelState.IsValid)
            {
                var entity = new Subject
                {
                    SubjectId = subject.SubjectId,
                    SubjectName = subject.SubjectName,
                    DepartmentId = subject.DepartmentId
                };

                _service.Add(entity);
            }

            return Json(new[] { subject }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Subjects_Update([DataSourceRequest]DataSourceRequest request, Subject subject)
        {
            if (ModelState.IsValid)
            {
                var entity = new Subject
                {
                    SubjectId = subject.SubjectId,
                    SubjectName = subject.SubjectName,
                    DepartmentId = subject.DepartmentId
                };

                _service.Update(entity);
            }

            return Json(new[] { subject }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Subjects_Destroy([DataSourceRequest]DataSourceRequest request, Subject subject)
        {
            if (ModelState.IsValid)
            {
                var entity = new Subject
                {
                    SubjectId = subject.SubjectId,
                    SubjectName = subject.SubjectName,
                    DepartmentId = subject.DepartmentId
                };

                _service.Delete(entity);
            }

            return Json(new[] { subject }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            _departmentService.Dispose();
            base.Dispose(disposing);
        }
    }
}
