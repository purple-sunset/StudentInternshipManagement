using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Services.Interfaces;
using StudentInternshipManagement.Controllers;

namespace StudentInternshipManagement.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentController : BaseController
    {
        private readonly ILearningClassService _learningClassService;
        private readonly IStudentClassService _studentClassService;
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService, IStudentClassService studentClassService,
            ILearningClassService learningClassService)
        {
            _studentService = studentService;
            _studentClassService = studentClassService;
            _learningClassService = learningClassService;
        }

        public ActionResult Index()
        {
            ViewBag.StudentClasses = _studentClassService.GetAll();
            ViewBag.LearningClasses = _learningClassService.GetAll();
            return View();
        }

        public ActionResult Students_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = _studentService.GetAll().ToDataSourceResult(request, student => new
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Students_Create([DataSourceRequest] DataSourceRequest request,
            global::Models.Entities.Student student)
        {
            if (ModelState.IsValid)
            {
                _studentService.Add(student);
            }

            return Json(new[] {student}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Students_Update([DataSourceRequest] DataSourceRequest request,
            global::Models.Entities.Student student)
        {
            if (ModelState.IsValid)
            {
                _studentService.Update(student);
            }

            return Json(new[] {student}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Students_Destroy([DataSourceRequest] DataSourceRequest request,
            global::Models.Entities.Student student)
        {
            if (ModelState.IsValid)
            {
                _studentService.Delete(student);
            }

            return Json(new[] {student}.ToDataSourceResult(request, ModelState));
        }

        public ActionResult GetLearningClassList(int studentId, [DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = _studentService.GetById(studentId).LearningClassStudents.ToDataSourceResult(
                request, student => new
                {
                    student.StudentId,
                    student.ClassId,
                    student.MidTermPoint,
                    student.EndTermPoint,
                    student.TotalPoint
                });

            return Json(result);
        }
    }
}