using StudentInternshipManagement.Services.Implements;
using StudentInternshipManagement.Web.Controllers;
using Unity;

namespace StudentInternshipManagement.Web.Areas.Teacher.Controllers
{
    public class TeacherBaseController : BaseController
    {
        private readonly ITeacherService _teacherService;

        public TeacherBaseController()
        {
            _teacherService = UnityConfig.Container.Resolve<ITeacherService>();
        }

        public TeacherBaseController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public int CurrentTeacherId => CurrentTeacher.Id;

        public Models.Entities.Teacher CurrentTeacher
        {
            get
            {
                var userName = CurrentUser.UserName;
                return _teacherService.GetByUserName(userName);
            }
        }

        protected ITeacherService TeacherService => _teacherService;
    }
}