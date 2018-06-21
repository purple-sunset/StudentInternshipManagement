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
    public class CompanyController : Controller
    {
        private readonly CompanyService _service=new CompanyService();
        private readonly TrainingMajorService _trainingMajorService = new TrainingMajorService();

        public ActionResult Index()
        {
            ViewBag.TrainingMajors = _trainingMajorService.GetAll();
            return View();
        }

        public ActionResult Companies_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _service.GetAll().ToDataSourceResult(request, company => new {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
                CompanyDescription = company.CompanyDescription,
                Address = company.Address,
                Email = company.Email,
                Phone = company.Phone
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Companies_Create([DataSourceRequest]DataSourceRequest request, Company company)
        {
            if (ModelState.IsValid)
            {
                var entity = new Company
                {
                    CompanyName = company.CompanyName,
                    CompanyDescription = company.CompanyDescription,
                    Address = company.Address,
                    Email = company.Email,
                    Phone = company.Phone
                };

                _service.Add(entity);
            }

            return Json(new[] { company }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Companies_Update([DataSourceRequest]DataSourceRequest request, Company company)
        {
            if (ModelState.IsValid)
            {
                var entity = new Company
                {
                    CompanyId = company.CompanyId,
                    CompanyName = company.CompanyName,
                    CompanyDescription = company.CompanyDescription,
                    Address = company.Address,
                    Email = company.Email,
                    Phone = company.Phone
                };

                _service.Update(entity);
            }

            return Json(new[] { company }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Companies_Destroy([DataSourceRequest]DataSourceRequest request, Company company)
        {
            if (ModelState.IsValid)
            {
                var entity = new Company
                {
                    CompanyId = company.CompanyId,
                    CompanyName = company.CompanyName,
                    CompanyDescription = company.CompanyDescription,
                    Address = company.Address,
                    Email = company.Email,
                    Phone = company.Phone
                };

                _service.Delete(entity);
            }

            return Json(new[] { company }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            _trainingMajorService.Dispose();
            base.Dispose(disposing);
        }
    }
}
