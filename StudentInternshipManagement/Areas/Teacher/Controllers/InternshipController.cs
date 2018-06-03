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

namespace StudentInternshipManagement.Areas.Teacher.Controllers
{
    public class InternshipController : Controller
    {
        private readonly LearningClassStudentService _service = new LearningClassStudentService();
        private readonly StudentService _studentService = new StudentService();
        private readonly LearningClassService _learningClassService = new LearningClassService();

        public ActionResult Index()
        {
            ViewBag.Students = _studentService.GetAll();
            ViewBag.LearningClasses = _learningClassService.GetAll();
            return View();
        }

        public ActionResult LearningClassStudents_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _service.GetAll().ToDataSourceResult(request, learningClassStudent => new {
                ClassId = learningClassStudent.ClassId,
                StudentId = learningClassStudent.StudentId,
                MidTermPoint = learningClassStudent.MidTermPoint,
                EndTermPoint = learningClassStudent.EndTermPoint,
                TotalPoint = learningClassStudent.TotalPoint
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LearningClassStudents_Update([DataSourceRequest]DataSourceRequest request, LearningClassStudent learningClassStudent)
        {
            if (ModelState.IsValid)
            {
                var entity = new LearningClassStudent
                {
                    ClassId = learningClassStudent.ClassId,
                    StudentId = learningClassStudent.StudentId,
                    MidTermPoint = learningClassStudent.MidTermPoint,
                    EndTermPoint = learningClassStudent.EndTermPoint,
                    TotalPoint = learningClassStudent.TotalPoint
                };

                _service.Update(entity);
            }

            return Json(new[] { learningClassStudent }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            _studentService.Dispose();
            _learningClassService.Dispose();
            base.Dispose(disposing);
        }
    }
}
