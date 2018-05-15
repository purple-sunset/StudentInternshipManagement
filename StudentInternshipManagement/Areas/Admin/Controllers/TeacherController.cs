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
    public class TeacherController : Controller
    {
        //private WebContext db = new WebContext();
        private readonly TeacherService  _service=new TeacherService();
        private readonly DepartmentService _departmentService = new DepartmentService();

        public ActionResult Index()
        {
            ViewBag.Department = _departmentService.GetAll();
            return View();
        }

        public ActionResult Teachers_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _service.GetAll().ToDataSourceResult(request, teacher => new {
                TeacherId = teacher.TeacherId,
                TeacherName = teacher.TeacherName,
                BirthDate = teacher.BirthDate,
                Address = teacher.Address,
                Phone = teacher.Phone,
                DepartmentId=teacher.DepartmentId
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Teachers_Create([DataSourceRequest]DataSourceRequest request, global::Models.Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                var entity = new global::Models.Teacher
                {
                    TeacherName = teacher.TeacherName,
                    BirthDate = teacher.BirthDate,
                    Address = teacher.Address,
                    Phone = teacher.Phone,
                    DepartmentId = teacher.DepartmentId
                };

                _service.Add(entity);
            }

            return Json(new[] { teacher }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Teachers_Update([DataSourceRequest]DataSourceRequest request, global::Models.Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                var entity = new global::Models.Teacher
                {
                    TeacherId = teacher.TeacherId,
                    TeacherName = teacher.TeacherName,
                    BirthDate = teacher.BirthDate,
                    Address = teacher.Address,
                    Phone = teacher.Phone,
                    DepartmentId = teacher.DepartmentId
                };

                _service.Update(teacher);
            }

            return Json(new[] { teacher }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Teachers_Destroy([DataSourceRequest]DataSourceRequest request, global::Models.Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                var entity = new global::Models.Teacher
                {
                    TeacherId = teacher.TeacherId,
                    TeacherName = teacher.TeacherName,
                    BirthDate = teacher.BirthDate,
                    Address = teacher.Address,
                    Phone = teacher.Phone,
                    DepartmentId = teacher.DepartmentId
                };

                _service.Delete(teacher);
            }

            return Json(new[] { teacher }.ToDataSourceResult(request, ModelState));
        }

    }
}
