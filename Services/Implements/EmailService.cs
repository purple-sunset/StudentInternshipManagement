using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Models.Entities;
using Services.Interfaces;

namespace Services.Implements
{
    public class EmailService : IEmailService
    {
        private static readonly string FilePath = AppDomain.CurrentDomain.BaseDirectory + "\\Assignment";

        private static readonly string Header =
            "MSSV;Tên sinh viên;Lớp;Chương trình đào tạo;Khoa;Mã môn học;Công ty;Định hướng;Giáo viên hướng dẫn;Mã nhóm;Nhóm trưởng";

        private readonly ICompanyService _companyService;
        private readonly ICompanyTrainingMajorService _companyTrainingMajorService;
        private readonly IEmailHistoryService _emailHistoryService;
        private readonly IGroupService _groupService;

        private readonly IInternshipService _internshipService;
        private readonly ISemesterService _semesterService;
        private readonly ITeacherService _teacherService;

        public EmailService(IInternshipService internshipService, ITeacherService teacherService,
            ISemesterService semesterService, IGroupService groupService,
            ICompanyTrainingMajorService companyTrainingMajorService, ICompanyService companyService,
            IEmailHistoryService emailHistoryService)
        {
            _internshipService = internshipService;
            _teacherService = teacherService;
            _semesterService = semesterService;
            _groupService = groupService;
            _companyTrainingMajorService = companyTrainingMajorService;
            _companyService = companyService;
            _emailHistoryService = emailHistoryService;
        }

        public void SendCreateEmail()
        {
            throw new NotImplementedException();
        }

        public void SendProcessEmail()
        {
            CreateProcessAttachment();

            var mailToCompanies = CreateProcessEmailToCompany();
            foreach (var mail in mailToCompanies) _emailHistoryService.CreateAndSend(mail);

            var mailToTeachers = CreateProcessEmailToTeacher();
            foreach (var mail in mailToTeachers) _emailHistoryService.CreateAndSend(mail);

            var mailToStudents = CreateProcessEmailToStudent();
            _emailHistoryService.CreateAndSend(mailToStudents);
        }


        private void CreateProcessAttachment()
        {
            var semester = _semesterService.GetLatest().Id;
            var groups = _groupService.GetBySemester(semester);
            var path = $"{FilePath}\\{semester}";
            Directory.CreateDirectory($"{FilePath}");
            Directory.CreateDirectory(path);
            var companies = _companyTrainingMajorService.GetAll()
                .Where(c => c.AvailableTraineeCount < c.TotalTraineeCount);
            foreach (var comp in companies)
            {
                var grByComp = groups.Where(g => g.CompanyId == comp.CompanyId);
                if (grByComp.Any())
                    File.WriteAllText($"{path}\\{comp.Company.CompanyName}.csv", CreateProcessCSV(grByComp),
                        new UTF8Encoding(true));
            }

            var teachers = _teacherService.GetAll();
            foreach (var tc in teachers)
            {
                var grByTeacher = groups.Where(g => g.TeacherId == tc.Id);
                if (grByTeacher.Any())
                    File.WriteAllText($"{path}\\{tc.Id}.csv", CreateProcessCSV(grByTeacher),
                        new UTF8Encoding(true));
            }
        }

        // ReSharper disable once InconsistentNaming
        private string CreateProcessCSV(IQueryable<Group> groups)
        {
            var sb = new StringBuilder();
            sb.AppendLine(Header);
            foreach (var gr in groups)
            foreach (var mb in gr.Members)
            {
                var line = string.Join(";", mb.Id.ToString(), mb.StudentName, mb.Class.ClassName, mb.Program,
                    mb.Class.Department.DepartmentName, gr.Class.SubjectId.ToString(), gr.Major.Company.CompanyName,
                    gr.Major.TrainingMajor.TrainingMajorName, gr.Teacher.TeacherName, gr.Id.ToString(),
                    gr.LeaderId == mb.Id ? "x" : "");
                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        private List<MailMessage> CreateProcessEmailToCompany()
        {
            var result = new List<MailMessage>();

            var semester = _semesterService.GetLatest().Id;
            var path = $"{FilePath}\\{semester}";
            var fileNames = Directory.GetFiles(path).Select(f => Path.GetFileNameWithoutExtension(f));
            var companies = _companyService.GetAll().Where(t => fileNames.Contains(t.CompanyName));

            foreach (var cp in companies)
            {
                var mail = new MailMessage();
                mail.Subject = "Danh sách sinh viên thực tập Trường đại học Bách Khoa Hà Nội";
                mail.From = new MailAddress(ConfigurationManager.AppSettings["mailAccount"]);
                var emailBody = new StringBuilder();
                emailBody.AppendLine("Trường đại học Bách Khoa Hà Nội gửi danh sách sinh viên thực tập");
                emailBody.Append("Xem tệp đính kèm để xem thông tin thực tập kỳ này");
                mail.IsBodyHtml = true;
                mail.Body = emailBody.ToString();
                mail.Attachments.Add(new Attachment($"{path}\\{cp.CompanyName}.csv"));
                mail.To.Add(new MailAddress(cp.Email));
                result.Add(mail);
            }

            return result;
        }

        private List<MailMessage> CreateProcessEmailToTeacher()
        {
            var result = new List<MailMessage>();

            var semester = _semesterService.GetLatest().Id;
            var path = $"{FilePath}\\{semester}";
            var fileNames = Directory.GetFiles(path).Select(f => Path.GetFileNameWithoutExtension(f));
            var teachers = _teacherService.GetAll().Where(t => fileNames.Contains(t.Id.ToString()));

            foreach (var tc in teachers)
            {
                var mail = new MailMessage();
                mail.Subject = $"Thông tin phân công hướng dẫn thực tập kỳ {semester}";
                mail.From = new MailAddress(ConfigurationManager.AppSettings["mailAccount"]);
                var emailBody = new StringBuilder();
                emailBody.AppendLine("Trường đại học Bách Khoa Hà Nội gửi danh sách sinh viên thực tập");
                emailBody.Append(
                    @"<p>Nhấn vào liên kết hoặc xem tệp đính kèm để xem thông tin thực tập kỳ này: <a href = '" +
                    @"http://sim.hust.edu.vn/Teacher/Internship" + "'> thực tập </a></p>");
                mail.IsBodyHtml = true;
                mail.Body = emailBody.ToString();
                mail.Attachments.Add(new Attachment($"{path}\\{tc.Id}.csv"));
                mail.To.Add(new MailAddress(tc.User.Email));
                result.Add(mail);
            }

            return result;
        }

        private MailMessage CreateProcessEmailToStudent()
        {
            var students = _internshipService.GetByLatestSemester().Select(i => i.Student);
            var semester = _semesterService.GetLatest().Id;
            var mail = new MailMessage();

            foreach (var st in students)
            {
                mail.Subject = $"Thông tin phân công hướng dẫn thực tập kỳ {semester}";
                mail.From = new MailAddress(ConfigurationManager.AppSettings["mailAccount"]);
                var emailBody = new StringBuilder();
                emailBody.AppendLine("Trường đại học Bách Khoa Hà Nội gửi kết quả đăng ký thực tâp");
                emailBody.Append(@"<p>Nhấn vào liên kết để xem thông tin thực tập kỳ này: <a href = '" +
                                 @"http://sim.hust.edu.vn/Student/Internship" + "'> thực tập </a></p>");
                mail.IsBodyHtml = true;
                mail.Body = emailBody.ToString();
                mail.To.Add(new MailAddress(st.User.Email));
            }

            return mail;
        }
    }
}