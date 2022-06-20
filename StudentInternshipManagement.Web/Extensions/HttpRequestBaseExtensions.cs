using Microsoft.AspNet.Identity;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Services.Implements;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Unity;

namespace StudentInternshipManagement.Web.Extensions
{
    public static class HttpRequestBaseExtensions
    {
        public static ApplicationUser CurrentUser(this HttpRequestBase request)
        {
            return request.RequestContext.HttpContext.CurrentUser();
        }

        public static ApplicationUser CurrentUser(this HttpContextBase httpContext)
        {
            string id = httpContext.User?.Identity?.GetUserId();
            if (id != null)
            {
                var userService = UnityConfig.Container.Resolve<IUserService>();
                return userService.GetById(id);
            }
            return null;
        }

        public static string CurrentRole(this HttpRequestBase request)
        {
            return request.RequestContext.HttpContext.CurrentRole();
        }

        public static string CurrentRole(this HttpContextBase httpContext)
        {
            if (httpContext.User != null && httpContext.User.Identity != null)
            {
                return ((ClaimsIdentity)httpContext.User.Identity).Claims.Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).FirstOrDefault();
            }
            return string.Empty;
        }
    }
}