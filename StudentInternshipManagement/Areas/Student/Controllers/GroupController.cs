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
 using Models.Entities;
 using Services;
 using Services.Implements;

namespace StudentInternshipManagement.Areas.Student.Controllers
{
    public class GroupController : Controller
    {
        private readonly GroupService _service=new GroupService();
        private readonly TrainingMajorService _trainingMajorService = new TrainingMajorService();
        private readonly LearningClassService _learningClassService = new LearningClassService();
        private readonly CompanyService _companyService = new CompanyService();
        private readonly StudentService _studentService = new StudentService();
        private readonly TeacherService _teacherService = new TeacherService();

        public ActionResult Index()
        {
            ViewBag.Companies = _companyService.GetAll();
            ViewBag.TrainingMajors = _trainingMajorService.GetAll();
            ViewBag.Students = _studentService.GetAll();
            ViewBag.Teachers = _teacherService.GetAll();
            ViewBag.Classes = _learningClassService.GetAll();
            return View();
        }

        public ActionResult Groups_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Group> groups = _service.GetAll();
            DataSourceResult result = groups.ToDataSourceResult(request, group => new {
                GroupId = group.GroupId,
                GroupName = group.GroupName,
                ClassId = group.ClassId,
                CompanyId = group.CompanyId,
                TrainingMajorId = group.TrainingMajorId,
                LeaderId = group.LeaderId,
                TeacherId = group.TeacherId,
            });

            return Json(result);
        }

        public ActionResult GetStudentList(int groupId, [DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _service.GetById(groupId).Members.ToDataSourceResult(request, student => new {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                BirthDate = student.BirthDate,
                Address = student.Address,
                Phone = student.Phone,
                Cpa = student.Cpa,
                ClassId = student.ClassId
            });

            return Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            _teacherService.Dispose();
            _companyService.Dispose();
            _studentService.Dispose();
            _trainingMajorService.Dispose();
            _learningClassService.Dispose();
            base.Dispose(disposing);
        }
    }
}
