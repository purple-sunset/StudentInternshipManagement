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
    [Authorize(Roles = "Admin")]
    public class TrainingMajorController : Controller
    {
        private readonly TrainingMajorService _service=new TrainingMajorService();
        private readonly SubjectService _subjectService=new SubjectService();
        private readonly CompanyService _companyService = new CompanyService();

        public ActionResult Index()
        {
            ViewBag.Subjects = _subjectService.GetAll();
            ViewBag.Companies = _companyService.GetAll();
            return View();
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TrainingMajors_Create([DataSourceRequest]DataSourceRequest request, TrainingMajor trainingMajor)
        {
            if (ModelState.IsValid)
            {
                var entity = new TrainingMajor
                {
                    TrainingMajorName = trainingMajor.TrainingMajorName,
                    SubjectId = trainingMajor.SubjectId
                };

                _service.Add(entity);
            }

            return Json(new[] { trainingMajor }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TrainingMajors_Update([DataSourceRequest]DataSourceRequest request, TrainingMajor trainingMajor)
        {
            if (ModelState.IsValid)
            {
                var entity = new TrainingMajor
                {
                    TrainingMajorId = trainingMajor.TrainingMajorId,
                    TrainingMajorName = trainingMajor.TrainingMajorName,
                    SubjectId = trainingMajor.SubjectId
                };

                _service.Add(entity);
            }

            return Json(new[] { trainingMajor }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TrainingMajors_Destroy([DataSourceRequest]DataSourceRequest request, TrainingMajor trainingMajor)
        {
            if (ModelState.IsValid)
            {
                var entity = new TrainingMajor
                {
                    TrainingMajorId = trainingMajor.TrainingMajorId,
                    TrainingMajorName = trainingMajor.TrainingMajorName,
                    SubjectId = trainingMajor.SubjectId
                };

                _service.Delete(entity);
            }

            return Json(new[] { trainingMajor }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult CompanyTrainingMajors_Read(int trainingMajorId, [DataSourceRequest]DataSourceRequest request)
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
            DataSourceResult result = _service.GetCompanyList(majorId).ToDataSourceResult(request, company => new {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
                CompanyDescription = company.CompanyDescription,
                Address = company.Address,
                Email = company.Email,
                Phone = company.Phone
            });

            return Json(result);
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
