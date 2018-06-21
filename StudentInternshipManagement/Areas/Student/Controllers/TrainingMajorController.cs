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

namespace StudentInternshipManagement.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class TrainingMajorController : Controller
    {
        private readonly TrainingMajorService _service=new TrainingMajorService();
        private readonly SubjectService _subjectService=new SubjectService();
        private readonly CompanyService _companyService = new CompanyService();

        public ActionResult Index()
        {
            ViewBag.Subjects = _subjectService.GetAll();
            ViewBag.Companies = _companyService.GetAll();
            return null;
        }

        public ActionResult TrainingMajors_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _service.GetAll().ToDataSourceResult(request, trainingMajor => new {
                TrainingMajorId = trainingMajor.TrainingMajorId,
                TrainingMajorName = trainingMajor.TrainingMajorName,
                SubjectId = trainingMajor.SubjectId
            });

            return Json(result);
        }

        public ActionResult GetCompanyList(int majorId, [DataSourceRequest]DataSourceRequest request)
        {
            return Json(_service.GetCompanyList(majorId), JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            _subjectService.Dispose();
            _companyService.Dispose();
            base.Dispose(disposing);
        }
    }
}
