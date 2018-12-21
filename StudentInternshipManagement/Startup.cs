using System;
using System.Web.Mvc;
using Hangfire;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Models.Entities;
using Owin;
using Services.Implements;
using Unity;

[assembly: OwinStartupAttribute(typeof(StudentInternshipManagement.Startup))]
namespace StudentInternshipManagement
{
    public partial class Startup
    {
        public static IDataProtectionProvider DataProtectionProvider { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            DataProtectionProvider = app.GetDataProtectionProvider();
            app.CreatePerOwinContext(() => UnityConfig.Container.Resolve<ApplicationUserManager>());

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(UnityConfig.Container.Resolve<CookieAuthenticationOptions>());

            GlobalConfiguration.Configuration.UseSqlServerStorage("StudentInternshipManagement");
            app.UseHangfireDashboard("/jobs", UnityConfig.Container.Resolve<DashboardOptions>());
            app.UseHangfireServer();
        }
    }
}
