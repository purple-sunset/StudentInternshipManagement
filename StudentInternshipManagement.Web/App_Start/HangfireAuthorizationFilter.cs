using Hangfire.Dashboard;
using Microsoft.Owin.Security;

namespace StudentInternshipManagement.Web
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