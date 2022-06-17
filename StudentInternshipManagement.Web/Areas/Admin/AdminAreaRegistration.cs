﻿using System.Web.Mvc;

namespace StudentInternshipManagement.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Admin";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional},
                new[] { "StudentInternshipManagement.Web.Areas.Admin.Controllers" }
            );
        }
    }
}