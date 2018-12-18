using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Models;
using Models.Entities;

namespace StudentInternshipManagement.Controllers
{
    public class BaseController:Controller
    {
        public ApplicationUser CurrentUser
        {
            get
            {
                var id = User.Identity.GetUserName();
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                return userManager.FindByName(id);
            }
        }

        public string CurrentRole
        {
            get
            {
                return ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();
            }
        }
    }
}