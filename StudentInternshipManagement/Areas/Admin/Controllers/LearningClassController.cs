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
            return View();
        }

        public ActionResult LearningClasses_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _service.GetAll().ToDataSourceResult(request, learningClass => new {
                ClassId = learningClass.ClassId,
                ClassName = learningClass.ClassName,
                SubjectId = learningClass.SubjectId,
                SemesterId = learningClass.SemesterId
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LearningClasses_Create([DataSourceRequest]DataSourceRequest request, LearningClass learningClass)
        {
            if (ModelState.IsValid)
            {
                var entity = new LearningClass
                {
                    ClassName = learningClass.ClassName,
                    SubjectId = learningClass.SubjectId,
                    SemesterId = learningClass.SemesterId
                };

                _service.Add(entity);
            }

            return Json(new[] { learningClass }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LearningClasses_Update([DataSourceRequest]DataSourceRequest request, LearningClass learningClass)
        {
            if (ModelState.IsValid)
            {
                var entity = new LearningClass
                {
                    ClassId = learningClass.ClassId,
                    ClassName = learningClass.ClassName,
                    SubjectId = learningClass.SubjectId,
                    SemesterId = learningClass.SemesterId
                };

                _service.Update(entity);
            }

            return Json(new[] { learningClass }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LearningClasses_Destroy([DataSourceRequest]DataSourceRequest request, LearningClass learningClass)
        {
            if (ModelState.IsValid)
            {
                var entity = new LearningClass
                {
                    ClassId = learningClass.ClassId,
                    ClassName = learningClass.ClassName,
                    SubjectId = learningClass.SubjectId,
                    SemesterId = learningClass.SemesterId
                };

                _service.Delete(entity);
            }

            return Json(new[] { learningClass }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult GetStudentList(int classId, [DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _service.GetById(classId).LearningClassStudents.ToDataSourceResult(request, student => new {
                StudentId = student.StudentId,
                ClassId = student.ClassId,
                MidTermPoint = student.MidTermPoint,
                EndTermPoint = student.EndTermPoint,
                TotalPoint = student.TotalPoint
            });

            return Json(result);
        }

        public ActionResult GetTrainingMajorList(int classId, [DataSourceRequest]DataSourceRequest request)
        {
            var subject = _subjectService.GetById(_service.GetById(classId).SubjectId);
            DataSourceResult result = subject.TrainingMajors.ToDataSourceResult(request, trainingMajor => new {
                TrainingMajorId = trainingMajor.TrainingMajorId,
                TrainingMajorName = trainingMajor.TrainingMajorName,
                SubjectId = trainingMajor.SubjectId
            });

            return Json(result);
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
