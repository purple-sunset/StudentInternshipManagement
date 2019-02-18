using System;
using System.Linq;
using System.Web.Mvc;
using Hangfire;
using Hangfire.Storage;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Services.Implements;
using StudentInternshipManagement.Web.Controllers;

namespace StudentInternshipManagement.Web.Areas.Admin.Controllers
{
    public class InternshipController : AdminBaseController
    {
        private static int _semester = -1;
        private static string _jobId = string.Empty;

        private readonly IGroupService _groupService;
        private readonly IInternshipService _internshipService;
        private readonly ISemesterService _semesterService;

        public InternshipController(IInternshipService internshipService, IGroupService groupService,
            ISemesterService semesterService)
        {
            _internshipService = internshipService;
            _groupService = groupService;
            _semesterService = semesterService;
        }

        public ActionResult Index()
        {
            CheckJob();
            return View();
        }

        public ActionResult Process()
        {
            _jobId = BackgroundJob.Enqueue(() => _internshipService.ProcessRegistration());
            _semester = _semesterService.GetLatest().Id;
            return RedirectToAction("Index");
        }

        public void CheckJob()
        {
            int semesterId = _semesterService.GetLatest().Id;
            ViewBag.Semester = _semester;
            if (_semester != semesterId)
            {
                ViewBag.IsProcessing = null;
            }
            else
            {
                IStorageConnection connection = JobStorage.Current.GetConnection();
                JobData jobData = connection.GetJobData(_jobId);
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

                    // case "Failed":
                    default:
                        ViewBag.IsProcessing = null;
                        break;
                }
            }
        }

        public ActionResult Internships_Read([DataSourceRequest] DataSourceRequest request)
        {
            IQueryable<Internship> internships = _internshipService.GetByLatestSemester();
            DataSourceResult result = internships.ToDataSourceResult(request, internship => new
            {
                internship.Id,
                internship.RegistrationDate,
                internship.Status,
                Student = internship.Student.User.FullName,
                Class = internship.Class.ClassName,
                Company = internship.Major.Company.CompanyName,
                TrainingMajor = internship.Major.TrainingMajor.TrainingMajorName,
                internship.Student.LearningClassStudents.FirstOrDefault(l => l.ClassId == internship.ClassId)
                    ?.MidTermPoint,
                internship.Student.LearningClassStudents.FirstOrDefault(l => l.ClassId == internship.ClassId)
                    ?.EndTermPoint,
                internship.Student.LearningClassStudents.FirstOrDefault(l => l.ClassId == internship.ClassId)
                    ?.TotalPoint,
                Group = _groupService.GetByInternship(internship)?.GroupName,
                Teacher = _groupService.GetByInternship(internship)?.Teacher.User.FullName
            });

            return Json(result);
        }

        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            byte[] fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    }
}