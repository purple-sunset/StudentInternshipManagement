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
 using Services.Implements;
 using Services.Interfaces;

 namespace StudentInternshipManagement.Areas.Student.Controllers
{
    [Authorize (Roles="Student")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly ITrainingMajorService _trainingMajorService;

        public CompanyController(ICompanyService companyService, ITrainingMajorService trainingMajorService)
        {
            _companyService = companyService;
            _trainingMajorService = trainingMajorService;
        }

        public ActionResult Index()
        {
            ViewBag.TrainingMajors = _trainingMajorService.GetAll();
            return null;
        }

        public ActionResult Companies_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _companyService.GetAll().ToDataSourceResult(request, company => new
            {
                company.Id,
                company.CompanyName,
                company.CompanyDescription,
                company.Address,
                company.Email,
                company.Phone
            });

            return Json(result);
        }
    }
}
