﻿using System.Web.Mvc;

namespace StudentInternshipManagement.Web.Areas.Student
{
    public class StudentAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Student";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Student_default",
                "Student/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "StudentInternshipManagement.Web.Areas.Student.Controllers" }
            );
        }
    }
}