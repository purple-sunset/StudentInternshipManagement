using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Models.Entities;
using Services.Interfaces;
using Unity;

namespace StudentInternshipManagement.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUserService UserService;

        public BaseController()
        {
            UserService = UnityConfig.Container.Resolve<IUserService>();
        }

        public BaseController(IUserService userService)
        {
            UserService = userService;
        }

        public ApplicationUser CurrentUser
        {
            get
            {
                string id = User.Identity.GetUserId();
                return UserService.GetById(id);
            }
        }

        public string CurrentRole
        {
            get
            {
                return ((ClaimsIdentity) User.Identity).Claims.Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value).FirstOrDefault();
            }
        }
    }
}