using StudentInternshipManagement.Services.Implements;
using StudentInternshipManagement.Web.Controllers;
using Unity;

namespace StudentInternshipManagement.Web.Areas.Admin.Controllers
{
    public class AdminBaseController : BaseController
    {
        private readonly IAdminService _adminService;

        public AdminBaseController()
        {
            _adminService = UnityConfig.Container.Resolve<IAdminService>();
        }

        public AdminBaseController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public int CurrentAdminId => CurrentAdmin.Id;

        public Models.Entities.Admin CurrentAdmin
        {
            get
            {
                var userName = CurrentUser.UserName;
                return _adminService.GetByUserName(userName);
            }
        }

        protected IAdminService AdminService => _adminService;
    }
}