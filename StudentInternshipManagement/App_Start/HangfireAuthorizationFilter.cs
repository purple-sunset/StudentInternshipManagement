using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire.Dashboard;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace StudentInternshipManagement
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly IAuthenticationManager _authenticationManager;

        public HangfireAuthorizationFilter(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        public bool Authorize(DashboardContext context)
        {
            return _authenticationManager.User.IsInRole("Admin");
        }
    }
}