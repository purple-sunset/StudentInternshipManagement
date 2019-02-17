using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using StudentInternshipManagement.Models.Constants;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Services.ViewModel;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface IEmailService
    {
        void SendCreateEmail();
        void SendProcessEmail();
        Task SendResetPasswordMailAsync(ResetPasswordViewModel model);
    }

    #endregion

    #region Class

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

        public async Task SendResetPasswordMailAsync(ResetPasswordViewModel model)
        {
            var mail = new MailMessage();
            mail.To.Add(new MailAddress(model.Email));
            mail.Subject = "Reset Mật khẩu trên trang quản lý thực tập";
            var emailBody = new StringBuilder();
            string callbackUrl = $"{ForgotPasswordConstants.CallbackUrl}{model.Code}";
            emailBody.AppendLine("Trường đại học Bách Khoa Hà Nội gửi kết quả đăng ký thực tâp");
            emailBody.Append($@"<p>Nhấn vào < a href ='{callbackUrl}'>link</a> để reset mật khẩu</p>");
            mail.IsBodyHtml = true;
            mail.Body = emailBody.ToString();

            await _emailHistoryService.CreateAndSendAsync(mail);
        }

        public void SendCreateEmail()
        {
            throw new NotImplementedException();
        }

        public void SendProcessEmail()
        {
            CreateProcessAttachment();

            List<MailMessage> mailToCompanies = CreateProcessEmailToCompany();
            foreach (MailMessage mail in mailToCompanies) _emailHistoryService.CreateAndSend(mail);

            List<MailMessage> mailToTeachers = CreateProcessEmailToTeacher();
            foreach (MailMessage mail in mailToTeachers) _emailHistoryService.CreateAndSend(mail);

            MailMessage mailToStudents = CreateProcessEmailToStudent();
            _emailHistoryService.CreateAndSend(mailToStudents);
        }


        private void CreateProcessAttachment()
        {
            int semester = _semesterService.GetLatest().Id;
            IQueryable<Group> groups = _groupService.GetBySemester(semester);
            string path = $"{FilePath}\\{semester}";
            Directory.CreateDirectory($"{FilePath}");
            Directory.CreateDirectory(path);
            IQueryable<CompanyTrainingMajor> companies = _companyTrainingMajorService.GetAll()
                .Where(c => c.AvailableTraineeCount < c.TotalTraineeCount);
            foreach (CompanyTrainingMajor comp in companies)
            {
                IQueryable<Group> grByComp = groups.Where(g => g.CompanyId == comp.CompanyId);
                if (grByComp.Any())
                    File.WriteAllText($"{path}\\{comp.Company.CompanyName}.csv", CreateProcessCSV(grByComp),
                        new UTF8Encoding(true));
            }

            IQueryable<Teacher> teachers = _teacherService.GetAll();
            foreach (Teacher tc in teachers)
            {
                IQueryable<Group> grByTeacher = groups.Where(g => g.TeacherId == tc.Id);
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
            foreach (Group gr in groups)
                foreach (Student st in gr.Members)
                {
                    string line = string.Join(";", st.Id.ToString(), st.User.FullName, st.Class.ClassName, st.Program,
                        st.Class.Department.DepartmentName, gr.Class.SubjectId.ToString(), gr.Major.Company.CompanyName,
                        gr.Major.TrainingMajor.TrainingMajorName, gr.Teacher.User.FullName, gr.Id.ToString(),
                        gr.LeaderId == st.Id ? "x" : "");
                    sb.AppendLine(line);
                }

            return sb.ToString();
        }

        private List<MailMessage> CreateProcessEmailToCompany()
        {
            var result = new List<MailMessage>();

            int semester = _semesterService.GetLatest().Id;
            string path = $"{FilePath}\\{semester}";
            IEnumerable<string> fileNames = Directory.GetFiles(path).Select(f => Path.GetFileNameWithoutExtension(f));
            IQueryable<Company> companies = _companyService.GetAll().Where(t => fileNames.Contains(t.CompanyName));

            foreach (Company cp in companies)
            {
                var mail = new MailMessage();
                mail.Subject = "Danh sách sinh viên thực tập Trường đại học Bách Khoa Hà Nội";

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

            int semester = _semesterService.GetLatest().Id;
            string path = $"{FilePath}\\{semester}";
            IEnumerable<string> fileNames = Directory.GetFiles(path).Select(f => Path.GetFileNameWithoutExtension(f));
            IQueryable<Teacher> teachers = _teacherService.GetAll().Where(t => fileNames.Contains(t.Id.ToString()));

            foreach (Teacher tc in teachers)
            {
                var mail = new MailMessage();
                mail.Subject = $"Thông tin phân công hướng dẫn thực tập kỳ {semester}";

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
            IQueryable<Student> students = _internshipService.GetByLatestSemester().Select(i => i.Student);
            int semester = _semesterService.GetLatest().Id;
            var mail = new MailMessage();

            foreach (Student st in students)
            {
                mail.Subject = $"Thông tin phân công hướng dẫn thực tập kỳ {semester}";

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

    #endregion

}