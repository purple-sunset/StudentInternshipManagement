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
    public class CompanyTrainingMajorController : Controller
    {
        private readonly CompanyTrainingMajorService _service=new CompanyTrainingMajorService();
        private readonly TrainingMajorService _trainingMajorService = new TrainingMajorService();
        private readonly CompanyService _companyService = new CompanyService();

        public ActionResult Index()
        {
            ViewBag.Companies = _companyService.GetAll();
            ViewBag.TrainingMajors = _trainingMajorService.GetAll();
            return View();
        }

        public ActionResult CompanyTrainingMajors_Read([DataSourceRequest]DataSourceRequest request, int? companyId, int? majorId)
        {
            IQueryable<CompanyTrainingMajor> datasource = null;

            if (companyId != null)
            {
                
                datasource = _service.GetByCompany(companyId.Value);
            }
            else if (majorId != null)
            {
                
                datasource = _service.GetByTrainingMajor(majorId.Value);
            }
            else
            {
                datasource = _service.GetAll();
            }

            DataSourceResult result = datasource.ToDataSourceResult(request, companyTrainingMajor => new {
                CompanyId = companyTrainingMajor.CompanyId,
                TrainingMajorId = companyTrainingMajor.TrainingMajorId,
                TotalTraineeCount = companyTrainingMajor.TotalTraineeCount,
                AvailableTraineeCount = companyTrainingMajor.AvailableTraineeCount,
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CompanyTrainingMajors_Create([DataSourceRequest]DataSourceRequest request, int? companyId, int? majorId, CompanyTrainingMajor companyTrainingMajor)
        {
            if (ModelState.IsValid)
            {
                var entity = new CompanyTrainingMajor
                {
                    CompanyId = companyTrainingMajor.CompanyId,
                    TrainingMajorId = companyTrainingMajor.TrainingMajorId,
                    TotalTraineeCount = companyTrainingMajor.TotalTraineeCount,
                    AvailableTraineeCount = companyTrainingMajor.AvailableTraineeCount
                };

                if (companyId != null)
                {
                    entity.CompanyId = companyId.Value;
                }
                if (majorId != null)
                {
                    entity.TrainingMajorId = majorId.Value;
                }

                _service.Add(entity);
            }

            return Json(new[] { companyTrainingMajor }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CompanyTrainingMajors_Update([DataSourceRequest]DataSourceRequest request, CompanyTrainingMajor companyTrainingMajor)
        {
            if (ModelState.IsValid)
            {
                var entity = new CompanyTrainingMajor
                {
                    CompanyId = companyTrainingMajor.CompanyId,
                    TrainingMajorId = companyTrainingMajor.TrainingMajorId,
                    TotalTraineeCount = companyTrainingMajor.TotalTraineeCount,
                    AvailableTraineeCount = companyTrainingMajor.AvailableTraineeCount
                };

                _service.Update(entity);
            }

            return Json(new[] { companyTrainingMajor }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CompanyTrainingMajors_Destroy([DataSourceRequest]DataSourceRequest request, CompanyTrainingMajor companyTrainingMajor)
        {
            if (ModelState.IsValid)
            {
                var entity = new CompanyTrainingMajor
                {
                    CompanyId = companyTrainingMajor.CompanyId,
                    TrainingMajorId = companyTrainingMajor.TrainingMajorId,
                    TotalTraineeCount = companyTrainingMajor.TotalTraineeCount,
                    AvailableTraineeCount = companyTrainingMajor.AvailableTraineeCount
                };

                _service.Delete(entity);
            }

            return Json(new[] { companyTrainingMajor }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            _companyService.Dispose();
            _trainingMajorService.Dispose();
            base.Dispose(disposing);
        }
    }
}
