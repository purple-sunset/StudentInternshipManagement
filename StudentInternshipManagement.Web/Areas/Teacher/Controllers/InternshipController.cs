using System;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Services.Implements;

namespace StudentInternshipManagement.Web.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class InternshipController : TeacherBaseController
    {
        private readonly ILearningClassStudentService _learningClassStudentService;
        private readonly IStudentService _studentService;
        private readonly ILearningClassService _learningClassService;

        public InternshipController(ILearningClassStudentService learningClassStudentService, IStudentService studentService, ILearningClassService learningClassService)
        {
            _learningClassStudentService = learningClassStudentService;
            _studentService = studentService;
            _learningClassService = learningClassService;
        }

        public ActionResult Index()
        {
            ViewBag.Students = _studentService.GetAll();
            ViewBag.LearningClasses = _learningClassService.GetAll();
            return View();
        }

        public ActionResult LearningClassStudents_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _learningClassStudentService.GetByTeacher(CurrentTeacherId).ToDataSourceResult(request, learningClassStudent => new {
                ClassId = learningClassStudent.ClassId,
                StudentId = learningClassStudent.StudentId,
                MidTermPoint = learningClassStudent.MidTermPoint,
                EndTermPoint = learningClassStudent.EndTermPoint,
                TotalPoint = learningClassStudent.TotalPoint
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LearningClassStudents_Update([DataSourceRequest]DataSourceRequest request, LearningClassStudent learningClassStudent)
        {
            if (ModelState.IsValid)
            {
                var entity = new LearningClassStudent
                {
                    ClassId = learningClassStudent.ClassId,
                    StudentId = learningClassStudent.StudentId,
                    MidTermPoint = learningClassStudent.MidTermPoint,
                    EndTermPoint = learningClassStudent.EndTermPoint,
                    TotalPoint = learningClassStudent.TotalPoint
                };
                entity.TotalPoint = 0.3f * entity.MidTermPoint + 0.7f * entity.EndTermPoint;
                if (entity.TotalPoint != null)
                    entity.TotalPoint = (float) Math.Round(entity.TotalPoint.Value, 1);

                _learningClassStudentService.Update(entity);
            }

            return Json(new[] { learningClassStudent }.ToDataSourceResult(request, ModelState));
        }
    }
}
