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
 using Microsoft.AspNet.Identity;
 using Models;
 using Services;
 using Services.Implements;

namespace StudentInternshipManagement.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class LearningClassController : Controller
    {
        private readonly LearningClassService _service = new LearningClassService();
        private readonly StudentService _studentService = new StudentService();
        private readonly SubjectService _subjectService = new SubjectService();
        private readonly SemesterService _semesterService = new SemesterService();

        public ActionResult Index()
        {
            ViewBag.Subjects = _subjectService.GetAll();
            ViewBag.Semesters = _semesterService.GetAll();
            ViewBag.Students = _studentService.GetAll();
            return null;
        }

        public ActionResult LearningClasses_Read([DataSourceRequest]DataSourceRequest request)
        {
            var id = User.Identity.GetUserName();
            DataSourceResult result = _studentService.GetLearningClassBySemesterList(id).ToDataSourceResult(request, learningClass => new {
                ClassId = learningClass.ClassId,
                ClassName = learningClass.ClassName,
                SubjectId = learningClass.SubjectId,
                SemesterId = learningClass.SemesterId
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAll([DataSourceRequest]DataSourceRequest request)
        {
            var id = User.Identity.GetUserName();

            return Json(_studentService.GetLearningClassBySemesterList(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTrainingMajorList(int classId, [DataSourceRequest]DataSourceRequest request)
        {
            var subject = _subjectService.GetById(_service.GetById(classId).SubjectId);

            return Json(subject.TrainingMajors, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            _studentService.Dispose();
            _semesterService.Dispose();
            _subjectService.Dispose();
            base.Dispose(disposing);
        }
    }
}
