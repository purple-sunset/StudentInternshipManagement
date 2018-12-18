using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Models;
using Models.Entities;
using Repositories;
using Services.Interfaces;
using Utilities;

namespace Services.Implements
{
    public class EmailService:IEmailService
    {
        private static readonly string FilePath = AppDomain.CurrentDomain.BaseDirectory + "\\Assignment";

        private static readonly string Header =
            "MSSV;Tên sinh viên;Lớp;Chương trình đào tạo;Khoa;Mã môn học;Công ty;Định hướng;Giáo viên hướng dẫn;Mã nhóm;Nhóm trưởng";

        private readonly IInternshipService _internshipService;
        private readonly ITeacherService _teacherService;
        private readonly ISemesterService _semesterService;
        private readonly IGroupService _groupService;
        private readonly ICompanyTrainingMajorService _companyTrainingMajorService;
        private readonly ICompanyService _companyService;
        private readonly IUserService _userService;

        public EmailService(IInternshipService internshipService, ITeacherService teacherService, ISemesterService semesterService, IGroupService groupService, ICompanyTrainingMajorService companyTrainingMajorService, ICompanyService companyService, IUserService userService)
        {
            _internshipService = internshipService;
            _teacherService = teacherService;
            _semesterService = semesterService;
            _groupService = groupService;
            _companyTrainingMajorService = companyTrainingMajorService;
            _companyService = companyService;
            _userService = userService;
        }

        public void SendInternshipCreate()
        {
            CreateAttachment();
            SendEmailToStudent();
            SendEmailToTeacher();
            SendEmailToCompany();
        }


        private void CreateAttachment()
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
                {
                    File.WriteAllText($"{path}\\{comp.Company.CompanyName}.csv", CreateCsv(grByComp),
                        new UTF8Encoding(true));
                }
            }

            var teachers = (new TeacherRepository()).GetAll();
            foreach (var tc in teachers)
            {
                var grByTeacher = groups.Where(g => g.TeacherId == tc.Id);
                if (grByTeacher.Any())
                {
                    File.WriteAllText($"{path}\\{tc.Id}.csv", CreateCsv(grByTeacher),
                        new UTF8Encoding(true));
                }
            }
        }

        private string CreateCsv(IQueryable<Group> groups)
        {
            var sb = new StringBuilder();
            sb.AppendLine(Header);
            foreach (var gr in groups)
            {
                foreach (var mb in gr.Members)
                {
                    var line = string.Join(";", new string[]
                    {
                            mb.Id.ToString(),
                            mb.StudentName,
                            mb.Class.ClassName,
                            mb.Program,
                            mb.Class.Department.DepartmentName,
                            gr.Class.SubjectId.ToString(),
                            gr.Major.Company.CompanyName,
                            gr.Major.TrainingMajor.TrainingMajorName,
                            gr.Teacher.TeacherName,
                            gr.Id.ToString(),
                            gr.LeaderId == mb.Id ? "x" : ""
                    });
                    sb.AppendLine(line);
                }
            }

            return sb.ToString();
        }

        private void SendEmailToCompany()
        {
            var mail = new MailMessage();
            mail.Subject = "Danh sách sinh viên thực tập Trường đại học Bách Khoa Hà Nội";
            mail.From = new MailAddress(ConfigurationManager.AppSettings["mailAccount"]);
            StringBuilder emailBody = new StringBuilder();
            emailBody.AppendLine("Trường đại học Bách Khoa Hà Nội gửi danh sách sinh viên thực tập");
            emailBody.Append("Xem tệp đính kèm để xem thông tin thực tập kỳ này");
            mail.IsBodyHtml = true;
            mail.Body = emailBody.ToString();

            var semester = _semesterService.GetLatest().Id;
            var path = $"{FilePath}\\{semester}";
            var fileNames = Directory.GetFiles(path).Select(f => Path.GetFileNameWithoutExtension(f));
            var companies = _companyService.GetAll().Where(t => fileNames.Contains(t.CompanyName));
            foreach (var cp in companies)
            {
                mail.Attachments.Add(new Attachment($"{path}\\{cp.CompanyName}.csv"));
                mail.To.Add(new MailAddress(cp.Email));
                EmailSender.Send(mail);
                mail.Attachments.Clear();
                mail.To.Clear();
            }

        }

        private void SendEmailToTeacher()
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(ConfigurationManager.AppSettings["mailAccount"]);
            StringBuilder emailBody = new StringBuilder();
            emailBody.AppendLine("Trường đại học Bách Khoa Hà Nội gửi danh sách sinh viên thực tập");
            emailBody.Append(
                @"<p>Nhấn vào liên kết hoặc xem tệp đính kèm để xem thông tin thực tập kỳ này: <a href = '" +
                @"http://sim.hust.edu.vn/Teacher/Internship" + "'> thực tập </a></p>");
            mail.IsBodyHtml = true;
            mail.Body = emailBody.ToString();

            var semester = _semesterService.GetLatest().Id;
            var path = $"{FilePath}\\{semester}";
            var fileNames = Directory.GetFiles(path).Select(f => Path.GetFileNameWithoutExtension(f));
            mail.Subject = $"Thông tin phân công hướng dẫn thực tập kỳ {semester}";
            var teachers = _teacherService.GetAll().Where(t => fileNames.Contains(t.Id.ToString()));
            foreach (var tc in teachers)
            {
                mail.Attachments.Add(new Attachment($"{path}\\{tc.Id}.csv"));
                mail.To.Add(new MailAddress(_userService.GetByUserName(tc.TeacherCode).Email));
                EmailSender.Send(mail);
                mail.Attachments.Clear();
                mail.To.Clear();
            }

        }

        private void SendEmailToStudent()
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(ConfigurationManager.AppSettings["mailAccount"]);
            StringBuilder emailBody = new StringBuilder();
            emailBody.AppendLine("Trường đại học Bách Khoa Hà Nội gửi kết quả đăng ký thực tâp");
            emailBody.Append(@"<p>Nhấn vào liên kết để xem thông tin thực tập kỳ này: <a href = '" +
                             @"http://sim.hust.edu.vn/Student/Internship" + "'> thực tập </a></p>");
            mail.IsBodyHtml = true;
            mail.Body = emailBody.ToString();
            var students = _internshipService.GetByLatestSemester().Select(i => i.Student);


            var semester = _semesterService.GetLatest().Id;
            mail.Subject = $"Thông tin phân công hướng dẫn thực tập kỳ {semester}";
            foreach (var st in students)
            {
                mail.To.Add(new MailAddress(_userService.GetByUserName(st.StudentCode).Email));
            }


            EmailSender.Send(mail);

        }
    }
}
