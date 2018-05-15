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
    public class StudentController : Controller
    {
        //private WebContext db = new WebContext();
        private readonly StudentService _service=new StudentService();
        private readonly StudentClassService _classServiceservice = new StudentClassService();

        public ActionResult Index()
        {
            ViewBag.StudentClasses = _classServiceservice.GetAll();
            return View();
        }

        public ActionResult Students_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _service.GetAll().ToDataSourceResult(request, student => new {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                BirthDate = student.BirthDate,
                Address = student.Address,
                Phone = student.Phone,
                Cpa = student.Cpa,
                ClassId=student.ClassId
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Students_Create([DataSourceRequest]DataSourceRequest request, global::Models.Student student)
        {
            if (ModelState.IsValid)
            {
                var entity = new global::Models.Student
                {
                    StudentId = student.StudentId,
                    StudentName = student.StudentName,
                    BirthDate = student.BirthDate,
                    Address = student.Address,
                    Phone = student.Phone,
                    Cpa = student.Cpa,
                    ClassId = student.ClassId
                };

                _service.Add(entity);
            }

            return Json(new[] { student }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Students_Update([DataSourceRequest]DataSourceRequest request, global::Models.Student student)
        {
            if (ModelState.IsValid)
            {
                var entity = new global::Models.Student
                {
                    StudentId = student.StudentId,
                    StudentName = student.StudentName,
                    BirthDate = student.BirthDate,
                    Address = student.Address,
                    Phone = student.Phone,
                    Cpa = student.Cpa,
                    ClassId = student.ClassId
                };

                _service.Update(entity);
            }

            return Json(new[] { student }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Students_Destroy([DataSourceRequest]DataSourceRequest request, global::Models.Student student)
        {
            if (ModelState.IsValid)
            {
                var entity = new global::Models.Student
                {
                    StudentId = student.StudentId,
                    StudentName = student.StudentName,
                    BirthDate = student.BirthDate,
                    Address = student.Address,
                    Phone = student.Phone,
                    Cpa = student.Cpa,
                    ClassId = student.ClassId
                };

                _service.Delete(entity);
            }

            return Json(new[] { student }.ToDataSourceResult(request, ModelState));
        }

    }
}
