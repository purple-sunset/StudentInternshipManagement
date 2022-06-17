﻿using System.Web.Mvc;

namespace StudentInternshipManagement.Web.Areas.Teacher
{
    public class TeacherAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Teacher";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Teacher_default",
                "Teacher/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "StudentInternshipManagement.Web.Areas.Teacher.Controllers" }
            );
        }
    }
}