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
 using Services.Implements;
 using Services.Interfaces;

 namespace StudentInternshipManagement.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class TrainingMajorController : Controller
    {
        private readonly ITrainingMajorService _trainingMajorService;
        private readonly ISubjectService _subjectService;
        private readonly ICompanyService _companyService;

        public TrainingMajorController(ITrainingMajorService trainingMajorService, ISubjectService subjectService, ICompanyService companyService)
        {
            _trainingMajorService = trainingMajorService;
            _subjectService = subjectService;
            _companyService = companyService;
        }

        public ActionResult Index()
        {
            ViewBag.Subjects = _subjectService.GetAll();
            ViewBag.Companies = _companyService.GetAll();
            return null;
        }

        public ActionResult TrainingMajors_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _trainingMajorService.GetAll().ToDataSourceResult(request, trainingMajor => new {
                trainingMajor.Id,
                trainingMajor.TrainingMajorName,
                trainingMajor.SubjectId
            });

            return Json(result);
        }

        public ActionResult GetCompanyList(int majorId, [DataSourceRequest]DataSourceRequest request)
        {
            return Json(_trainingMajorService.GetCompanyList(majorId), JsonRequestBehavior.AllowGet);
        }
    }
}
