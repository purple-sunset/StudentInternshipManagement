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

namespace StudentInternshipManagement.Areas.Student.Controllers
{
    public class InternshipController : Controller
    {
        private readonly InternshipService _service = new InternshipService();
        private readonly StudentService _studentService = new StudentService();
        private readonly LearningClassService _learningClassService = new LearningClassService();
        private readonly CompanyService _companyService = new CompanyService();
        private readonly TrainingMajorService _trainingMajorService = new TrainingMajorService();

        public ActionResult Index()
        {
            ViewBag.Students = _studentService.GetAll();
            ViewBag.LearningClasses = _learningClassService.GetAll();
            ViewBag.Companies = _companyService.GetAll();
            ViewBag.TrainingMajors = _trainingMajorService.GetAll();
            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Students = _studentService.GetAll();
            ViewBag.LearningClasses = _learningClassService.GetAll();
            ViewBag.Companies = _companyService.GetAll();
            ViewBag.TrainingMajors = _trainingMajorService.GetAll();
            return View();
        }

        public ActionResult Internships_Read([DataSourceRequest]DataSourceRequest request)
        {
            var id = User.Identity.GetUserName();
            DataSourceResult result = _service.GetByStudent(id).ToDataSourceResult(request, internship => new {
                InternshipId = internship.InternshipId,
                RegistrationDate = internship.RegistrationDate,
                Status = internship.Status,
                StudentId = internship.StudentId,
                ClassId = internship.ClassId,
                CompanyId = internship.CompanyId,
                TrainingMajorId = internship.TrainingMajorId
            });

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Internships_Create(Internship internship)
        {

            if (ModelState.IsValid)
            {
                var entity = new Internship
                {
                    RegistrationDate = internship.RegistrationDate,
                    Status = internship.Status,
                    StudentId = internship.StudentId,
                    ClassId = internship.ClassId,
                    CompanyId = internship.CompanyId,
                    TrainingMajorId = internship.TrainingMajorId
                };

                ViewBag.Message = _service.Add(entity) ? "Thành công" : "Thất bại";
            }
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            _trainingMajorService.Dispose();
            _learningClassService.Dispose();
            _studentService.Dispose();
            base.Dispose(disposing);
        }
    }
}
