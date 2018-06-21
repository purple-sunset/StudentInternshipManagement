﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
 using System.Web.Security;
 using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Models;
 using Services;

namespace StudentInternshipManagement.Areas.Student.Controllers
{
    [Authorize (Roles="Student")]
    public class CompanyController : Controller
    {
        private readonly CompanyService _service=new CompanyService();
        private readonly TrainingMajorService _trainingMajorService = new TrainingMajorService();

        public ActionResult Index()
        {
            ViewBag.TrainingMajors = _trainingMajorService.GetAll();
            return null;
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

        

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            _trainingMajorService.Dispose();
            base.Dispose(disposing);
        }
    }
}
