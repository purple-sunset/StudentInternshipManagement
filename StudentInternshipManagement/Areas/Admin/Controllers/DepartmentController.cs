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
    public class DepartmentController : Controller
    {
        private readonly DepartmentService _service=new DepartmentService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Departments_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _service.GetAll().ToDataSourceResult(request, department => new {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Departments_Create([DataSourceRequest]DataSourceRequest request, Department department)
        {
            if (ModelState.IsValid)
            {
                var entity = new Department
                {
                    DepartmentName = department.DepartmentName
                };

                _service.Add(department);
            }

            return Json(new[] { department }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Departments_Update([DataSourceRequest]DataSourceRequest request, Department department)
        {
            if (ModelState.IsValid)
            {
                var entity = new Department
                {
                    DepartmentId = department.DepartmentId,
                    DepartmentName = department.DepartmentName
                };

                _service.Update(department);
            }

            return Json(new[] { department }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Departments_Destroy([DataSourceRequest]DataSourceRequest request, Department department)
        {
            if (ModelState.IsValid)
            {
                var entity = new Department
                {
                    DepartmentId = department.DepartmentId,
                    DepartmentName = department.DepartmentName
                };

                _service.Delete(department);
            }

            return Json(new[] { department }.ToDataSourceResult(request, ModelState));
        }

    }
}
