using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hangfire;
using Hangfire.Storage;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Models;
using Services;

namespace StudentInternshipManagement.Areas.Admin.Controllers
{
    public class InternshipController : Controller
    {
        private static int semester = -1;
        private static string jobId = string.Empty;

        private readonly InternshipService _service = new InternshipService();
        private readonly GroupService _groupService=new GroupService();
        private readonly SemesterService _semesterService = new SemesterService();

        public ActionResult Index()
        {
            CheckJob();
            return View();
        }

        public ActionResult Process()
        {
            jobId = BackgroundJob.Enqueue(() => _service.ProcessRegistration());
            semester = _semesterService.GetLatest().SemesterId;
            return RedirectToAction("Index");
        }

        public void CheckJob()
        {
            var semesterId = _semesterService.GetLatest().SemesterId;
            ViewBag.Semester = semester;
            if (semester != semesterId)
            {
                ViewBag.IsProcessing = null;
            }
            else
            {
                IStorageConnection connection = JobStorage.Current.GetConnection();
                JobData jobData = connection.GetJobData(jobId);
                string stateName = jobData.State;
                switch (stateName)
                {
                    case "Scheduled":
                    case "Awaiting":
                    case "Enqueued":
                        ViewBag.IsProcessing = true;
                        break;

                    case "Succeeded":
                        ViewBag.IsProcessing = false;
                        break;

                    case "Failed":
                    default:
                        ViewBag.IsProcessing = null;
                        break;

                }
            }
        }

        public ActionResult Internships_Read([DataSourceRequest]DataSourceRequest request)
        {
            var internships = _service.GetByLatestSemester();
            DataSourceResult result = internships.ToDataSourceResult(request, internship => new
            {
                InternshipId = internship.InternshipId,
                RegistrationDate = internship.RegistrationDate,
                Status = internship.Status,
                Student = internship.Student.StudentName,
                Class = internship.Class.ClassName,
                Company = internship.Major.Company.CompanyName,
                TrainingMajor = internship.Major.TrainingMajor.TrainingMajorName,
                MidTermPoint = internship.Student.LearningClassStudents.FirstOrDefault(l=>l.ClassId==internship.ClassId)?.MidTermPoint,
                EndTermPoint = internship.Student.LearningClassStudents.FirstOrDefault(l => l.ClassId == internship.ClassId)?.EndTermPoint,
                TotalPoint = internship.Student.LearningClassStudents.FirstOrDefault(l => l.ClassId == internship.ClassId)?.TotalPoint,
                Group = _groupService.GetByInternship(internship)?.GroupName,
                Teacher = _groupService.GetByInternship(internship)?.Teacher.TeacherName,
            });

            return Json(result);
        }

        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            _semesterService.Dispose();
            base.Dispose(disposing);
        }
    }
}
