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
    public class StudentClassController : Controller
    {
        private readonly StudentClassService _service=new StudentClassService();
        private readonly DepartmentService _departmentService = new DepartmentService();

        public ActionResult Index()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }

        public ActionResult StudentClasses_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _service.GetAll().ToDataSourceResult(request, studentClass => new {
                ClassId = studentClass.ClassId,
                ClassName = studentClass.ClassName,
                DepartmentId = studentClass.DepartmentId
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StudentClasses_Create([DataSourceRequest]DataSourceRequest request, StudentClass studentClass)
        {
            if (ModelState.IsValid)
            {
                var entity = new StudentClass
                {
                    ClassName = studentClass.ClassName,
                    DepartmentId = studentClass.DepartmentId
                };

                _service.Add(entity);
            }

            return Json(new[] { studentClass }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StudentClasses_Update([DataSourceRequest]DataSourceRequest request, StudentClass studentClass)
        {
            if (ModelState.IsValid)
            {
                var entity = new StudentClass
                {
                    ClassId = studentClass.ClassId,
                    ClassName = studentClass.ClassName,
                    DepartmentId = studentClass.DepartmentId
                };

                _service.Update(entity);
            }

            return Json(new[] { studentClass }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StudentClasses_Destroy([DataSourceRequest]DataSourceRequest request, StudentClass studentClass)
        {
            if (ModelState.IsValid)
            {
                var entity = new StudentClass
                {
                    ClassId = studentClass.ClassId,
                    ClassName = studentClass.ClassName,
                    DepartmentId = studentClass.DepartmentId
                };

                _service.Delete(entity);
            }

            return Json(new[] { studentClass }.ToDataSourceResult(request, ModelState));
        }

    }
}
