using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using StudentInternshipManagement.Services.Implements;

namespace StudentInternshipManagement.Web.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class LearningClassController : StudentBaseController
    {
        private readonly ILearningClassService _learningClassService;
        private readonly ISemesterService _semesterService;
        private readonly IStudentService _studentService;
        private readonly ISubjectService _subjectService;

        public LearningClassController(ILearningClassService learningClassService, IStudentService studentService,
            ISubjectService subjectService, ISemesterService semesterService)
        {
            _learningClassService = learningClassService;
            _studentService = studentService;
            _subjectService = subjectService;
            _semesterService = semesterService;
        }

        public ActionResult Index()
        {
            ViewBag.Subjects = _subjectService.GetAll();
            ViewBag.Semesters = _semesterService.GetAll();
            ViewBag.Students = _studentService.GetAll();
            return null;
        }

        public ActionResult LearningClasses_Read([DataSourceRequest] DataSourceRequest request)
        {
            var id = User.Identity.GetUserName();
            var result = _studentService.GetLearningClassBySemesterList(id).ToDataSourceResult(request, learningClass =>
                new
                {
                    ClassId = learningClass.Id,
                    learningClass.ClassName,
                    learningClass.SubjectId,
                    learningClass.SemesterId
                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var id = User.Identity.GetUserName();

            return Json(_studentService.GetLearningClassBySemesterList(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTrainingMajorList(int classId, [DataSourceRequest] DataSourceRequest request)
        {
            var subject = _subjectService.GetById(_learningClassService.GetById(classId).SubjectId);

            return Json(subject.TrainingMajors, JsonRequestBehavior.AllowGet);
        }
    }
}