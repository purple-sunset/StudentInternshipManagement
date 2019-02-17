using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using StudentInternshipManagement.Services.Implements;

namespace StudentInternshipManagement.Web.Areas.Student.Controllers
{
    public class GroupController : StudentBaseController
    {
        private readonly ICompanyService _companyService;
        private readonly IGroupService _groupService;
        private readonly ILearningClassService _learningClassService;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly ITrainingMajorService _trainingMajorService;

        public GroupController(IGroupService groupService, ITrainingMajorService trainingMajorService,
            ILearningClassService learningClassService, ICompanyService companyService, IStudentService studentService,
            ITeacherService teacherService)
        {
            _groupService = groupService;
            _trainingMajorService = trainingMajorService;
            _learningClassService = learningClassService;
            _companyService = companyService;
            _studentService = studentService;
            _teacherService = teacherService;
        }

        public ActionResult Index()
        {
            ViewBag.Companies = _companyService.GetAll();
            ViewBag.TrainingMajors = _trainingMajorService.GetAll();
            ViewBag.Students = _studentService.GetAll();
            ViewBag.Teachers = _teacherService.GetAll();
            ViewBag.Classes = _learningClassService.GetAll();
            return View();
        }

        public ActionResult Groups_Read([DataSourceRequest] DataSourceRequest request)
        {
            var groups = _groupService.GetAll();
            var result = groups.ToDataSourceResult(request, group => new
            {
                group.Id,
                group.GroupName,
                group.ClassId,
                group.CompanyId,
                group.TrainingMajorId,
                group.LeaderId,
                group.TeacherId
            });

            return Json(result);
        }

        public ActionResult GetStudentList(int groupId, [DataSourceRequest] DataSourceRequest request)
        {
            var result = _groupService.GetById(groupId).Members.ToDataSourceResult(request, student => new
            {
                student.Id,
                student.StudentCode,
                student.StudentName,
                student.BirthDate,
                student.Address,
                student.Phone,
                student.Cpa,
                student.ClassId
            });

            return Json(result);
        }
    }
}