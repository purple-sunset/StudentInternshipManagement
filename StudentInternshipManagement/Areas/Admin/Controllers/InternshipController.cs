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
    public class InternshipController : Controller
    {
        private readonly InternshipService _service=new InternshipService();
        private readonly StudentService _studentService=new StudentService();
        private readonly LearningClassService _learningClassService=new LearningClassService();
        private readonly CompanyService _companyService=new CompanyService();
        private readonly TrainingMajorService _trainingMajorService=new TrainingMajorService();

        public ActionResult Index()
        {
            ViewBag.Students = _studentService.GetAll();
            ViewBag.LearningClasses = _learningClassService.GetAll();
            ViewBag.Companies = _companyService.GetAll();
            ViewBag.TrainingMajors = _trainingMajorService.GetAll();
            return View();
        }

        public ActionResult Internships_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _service.GetAll().ToDataSourceResult(request, internship => new {
                InternshipId = internship.InternshipId,
                RegistrationDate = internship.RegistrationDate,
                Status = internship.Status,
                StudentId=internship.StudentId,
                ClassId = internship.ClassId,
                CompanyId = internship.CompanyId,
                TrainingMajorId = internship.TrainingMajorId
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Internships_Create([DataSourceRequest]DataSourceRequest request, Internship internship)
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

                _service.Add(entity);
            }

            return Json(new[] { internship }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Internships_Update([DataSourceRequest]DataSourceRequest request, Internship internship)
        {
            if (ModelState.IsValid)
            {
                var entity = new Internship
                {
                    InternshipId = internship.InternshipId,
                    RegistrationDate = internship.RegistrationDate,
                    Status = internship.Status,
                    StudentId = internship.StudentId,
                    ClassId = internship.ClassId,
                    CompanyId = internship.CompanyId,
                    TrainingMajorId = internship.TrainingMajorId
                };

                _service.Update(entity);
            }

            return Json(new[] { internship }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Internships_Destroy([DataSourceRequest]DataSourceRequest request, Internship internship)
        {
            if (ModelState.IsValid)
            {
                var entity = new Internship
                {
                    InternshipId = internship.InternshipId,
                    RegistrationDate = internship.RegistrationDate,
                    Status = internship.Status,
                    StudentId = internship.StudentId,
                    ClassId = internship.ClassId,
                    CompanyId = internship.CompanyId,
                    TrainingMajorId = internship.TrainingMajorId
                };

                _service.Delete(entity);
            }

            return Json(new[] { internship }.ToDataSourceResult(request, ModelState));
        }
    
        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
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
