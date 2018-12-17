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
 using StudentInternshipManagement.Controllers;

namespace StudentInternshipManagement.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;
        private readonly DepartmentService _departmentService;

        public AdminController(IAdminService adminService, DepartmentService departmentService)
        {
            _adminService = adminService;
            _departmentService = departmentService;
        }

        public ActionResult Index()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }

        public ActionResult Admins_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _adminService.GetAll().ToDataSourceResult(request, admin => new {
                AdminCode = admin.AdminCode,
                AdminName = admin.AdminName,
                BirthDate = admin.BirthDate,
                Address = admin.Address,
                Phone = admin.Phone,
                DepartmentId = admin.DepartmentId
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Admins_Create([DataSourceRequest]DataSourceRequest request, global::Models.Admin admin)
        {
            if (ModelState.IsValid)
            {
                var entity = new global::Models.Admin
                {
                    AdminName = admin.AdminName,
                    BirthDate = admin.BirthDate,
                    Address = admin.Address,
                    Phone = admin.Phone,
                    DepartmentId = admin.DepartmentId
                };

                _adminService.Add(admin);
            }

            return Json(new[] { admin }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Admins_Update([DataSourceRequest]DataSourceRequest request, global::Models.Admin admin)
        {
            if (ModelState.IsValid)
            {
                var entity = new global::Models.Admin
                {
                    AdminCode = admin.AdminCode,
                    AdminName = admin.AdminName,
                    BirthDate = admin.BirthDate,
                    Address = admin.Address,
                    Phone = admin.Phone,
                    DepartmentId = admin.DepartmentId
                };

                _adminService.Update(admin);
            }

            return Json(new[] { admin }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Admins_Destroy([DataSourceRequest]DataSourceRequest request, global::Models.Admin admin)
        {
            if (ModelState.IsValid)
            {
                var entity = new global::Models.Admin
                {
                    AdminCode = admin.AdminCode,
                    AdminName = admin.AdminName,
                    BirthDate = admin.BirthDate,
                    Address = admin.Address,
                    Phone = admin.Phone,
                    DepartmentId = admin.DepartmentId
                };

                _adminService.Delete(admin);
            }

            return Json(new[] { admin }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            _adminService.Dispose();
            _departmentService.Dispose();
            base.Dispose(disposing);
        }
    }
}
