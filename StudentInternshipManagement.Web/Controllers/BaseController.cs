using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Services.Implements;
using Unity;

namespace StudentInternshipManagement.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUserService _userService;

        public BaseController()
        {
            _userService = UnityConfig.Container.Resolve<IUserService>();
        }

        public BaseController(IUserService userService)
        {
            _userService = userService;
        }

        public string CurrentUserId => User.Identity.GetUserId();

        public ApplicationUser CurrentUser
        {
            get
            {
                string id = User.Identity.GetUserId();
                return _userService.GetById(id);
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

        protected IUserService UserService => _userService;
    }
}