using StudentInternshipManagement.Services.Implements;
using StudentInternshipManagement.Web.Controllers;
using Unity;

namespace StudentInternshipManagement.Web.Areas.Student.Controllers
{
    public class StudentBaseController : BaseController
    {
        private readonly IStudentService _studentService;

        public StudentBaseController()
        {
            _studentService = UnityConfig.Container.Resolve<IStudentService>();
        }

        public StudentBaseController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public int CurrentStudentId => CurrentStudent.Id;

        public Models.Entities.Student CurrentStudent
        {
            get
            {
                var userName = CurrentUser.UserName;
                return _studentService.GetByUserName(userName);
            }
        }

        protected IStudentService StudentService => _studentService;
    }
}