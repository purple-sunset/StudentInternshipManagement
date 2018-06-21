using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;
using Utilities;

namespace Services
{
    public class InternshipService : IDisposable
    {
        private readonly InternshipRepository _repository=new InternshipRepository();
        private static readonly string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Assignment";

        private static readonly string header =
            "MSSV;Tên sinh viên;Lớp;Chương trình đào tạo;Khoa;Mã môn học;Công ty;Định hướng;Giáo viên hướng dẫn;Mã nhóm;Nhóm trưởng";

        public IQueryable<Internship> GetAll()
        {
            return _repository.GetAll();
        }
        public IQueryable<Internship> GetBySemester(int semesterId)
        {
            return _repository.GetBySemester(semesterId);
        }

        public IQueryable<Internship> GetByLatestSemester()
        {
            using (var semesterRepository = new SemesterRepository())
            {
                var semesterId = semesterRepository.GetLatest().SemesterId;
                return _repository.GetBySemester(semesterId);
            }
        }
        public Internship GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Internship> GetByStudent(string studentId)
        {
            return _repository.GetByStudent(studentId);
        }

        public bool Add(Internship internship)
        {
            return _repository.Add(internship);
        }

        public bool Update(Internship internship)
        {
            return _repository.Update(internship);
        }

        public bool Delete(Internship internship)
        {
            return _repository.Delete(internship);
        }

        public void ProcessRegistration()
        {
            AssignInternship();
            CreateGroup();
            CreateAttachment();
            SendEmailToStudent();
            SendEmailToTeacher();
            SenEmailToCompany();
        }

        public void AssignInternship()
        {
            using (var majorRepository = new CompanyTrainingMajorRepository())
            {
                var lateRegisteredInternships = new List<Internship>();
                var leftMajors = majorRepository.GetAll().ToList();

                foreach (var item in GetByLatestSemester().OrderByDescending(i => i.RegistrationDate).ToList())
                {
                    if (item.Major.AvailableTraineeCount > 0)
                    {
                        item.Status = InternshipStatus.Success;
                        item.Major.AvailableTraineeCount--;
                        _repository.Update(item);
                    }
                    else
                    {
                        lateRegisteredInternships.Add(item);
                        leftMajors.Remove(item.Major);
                    }
                }

                foreach (var item in lateRegisteredInternships)
                {
                    var major = leftMajors.FirstOrDefault(m => m.TrainingMajorId == item.TrainingMajorId);
                    if (major != null)
                    {
                        item.TrainingMajorId = major.TrainingMajorId;
                        item.CompanyId = major.CompanyId;
                        item.Status = InternshipStatus.Success;
                        major.AvailableTraineeCount--;
                        if (major.AvailableTraineeCount == 0)
                        {
                            leftMajors.Remove(major);
                        }
                        _repository.Update(item);
                        lateRegisteredInternships.Remove(item);
                    }
                    else
                    {
                        var randomMajor = leftMajors.FirstOrDefault(m => m.TrainingMajor.SubjectId == item.Class.SubjectId);
                        if (randomMajor != null)
                        {
                            item.TrainingMajorId = randomMajor.TrainingMajorId;
                            item.CompanyId = randomMajor.CompanyId;
                            item.Status = InternshipStatus.Success;
                            randomMajor.AvailableTraineeCount--;
                            if (randomMajor.AvailableTraineeCount == 0)
                            {
                                leftMajors.Remove(randomMajor);
                            }
                            _repository.Update(item);
                            lateRegisteredInternships.Remove(item);
                        }
                    }
                }

                foreach (var item in lateRegisteredInternships)
                {
                    item.Status = InternshipStatus.Failed;
                    _repository.Update(item);
                }
            }
        }

        public void CreateGroup()
        {
            using (var groupRepository=new GroupRepository())
            {
                var groupByMajors = GetByLatestSemester().Where(i => i.Status == InternshipStatus.Success)
                        .GroupBy(i => i.Major).ToList();
                var teachers = (new TeacherRepository()).GetAll();
                var teacherAssign = teachers.ToDictionary(t => t, t => 6);
                foreach (var item in groupByMajors)
                {
                    var members = item.Select(i => i);
                    var groups = new List<List<Internship>>();
                    while (members.Any())
                    {
                        groups.Add(members.Take(5).ToList());
                        members = members.Skip(5);
                    }

                    var groupId = 1;
                    foreach (var groupItem in groups)
                    {
                        var group = new Group()
                        {
                            GroupName = $"{groupItem.FirstOrDefault().Major.Company.CompanyName}-{groupItem.FirstOrDefault().Major.TrainingMajor.TrainingMajorName}-{groupId}",
                            ClassId = groupItem.FirstOrDefault().Class.ClassId,
                            CompanyId = groupItem.FirstOrDefault().Major.CompanyId,
                            TrainingMajorId = groupItem.FirstOrDefault().Major.TrainingMajorId,
                            Members = groupItem.Select(g => g.Student).ToList(),
                            LeaderId = groupItem.OrderByDescending(g => g.Student.Cpa).FirstOrDefault().Student.StudentId,
                        };
                        var teacher = teacherAssign.FirstOrDefault(t =>
                            t.Key.Department.DepartmentId == groupItem.FirstOrDefault().Class.Subject.Department.DepartmentId && t.Value > 0).Key;
                        group.TeacherId = teacher.TeacherId;
                        (teacherAssign[teacher])--;
                        groupRepository.Add(group);
                        groupId++;
                    }
                } 
            }
        }

        public void CreateAttachment()
        {

            using (var groupRepository=new GroupRepository())
            {
                var semester = (new SemesterRepository()).GetLatest().SemesterId;
                var groups = groupRepository.GetBySemester(semester);
                var path = $"{filePath}\\{semester}";
                Directory.CreateDirectory($"{filePath}");
                Directory.CreateDirectory(path);
                var companies = (new CompanyTrainingMajorRepository()).GetAll().Where(c => c.AvailableTraineeCount < c.TotalTraineeCount);
                foreach (var comp in companies)
                {
                    var grByComp = groups.Where(g => g.CompanyId == comp.CompanyId);
                    if (grByComp != null && grByComp.Any())
                    {
                        File.WriteAllText($"{path}\\{comp.Company.CompanyName}.csv", CreateCsv(grByComp), new UTF8Encoding(true)); 
                    }
                }

                var teachers = (new TeacherRepository()).GetAll();
                foreach (var tc in teachers)
                {
                    var grByTeacher = groups.Where(g => g.TeacherId == tc.TeacherId);
                    if (grByTeacher != null && grByTeacher.Any())
                    {
                        File.WriteAllText($"{path}\\{tc.TeacherId}.csv", CreateCsv(grByTeacher), new UTF8Encoding(true));
                    }
                }
            }
        }

        public string CreateCsv(IQueryable<Group> groups)
        {
            var sb=new StringBuilder();
            sb.AppendLine(header);
            foreach (var gr in groups)
            {
                foreach (var mb in gr.Members)
                {
                    var line = string.Join(";", new string[]
                    {
                        mb.StudentId,
                        mb.StudentName,
                        mb.Class.ClassName,
                        mb.Program,
                        mb.Class.Department.DepartmentName,
                        gr.Class.SubjectId,
                        gr.Major.Company.CompanyName,
                        gr.Major.TrainingMajor.TrainingMajorName,
                        gr.Teacher.TeacherName,
                        gr.GroupId.ToString(),
                        gr.LeaderId.Equals(mb.StudentId) ? "x" : ""
                    });
                    sb.AppendLine(line);
                }
            }
            return sb.ToString();
        }

        public void SenEmailToCompany()
        {
            var mail = new MailMessage();
            mail.Subject = "Danh sách sinh viên thực tập Trường đại học Bách Khoa Hà Nội";
            mail.From = new MailAddress(ConfigurationManager.AppSettings["mailAccount"]);
            StringBuilder emailBody = new StringBuilder();
            emailBody.AppendLine("Trường đại học Bách Khoa Hà Nội gửi danh sách sinh viên thực tập");
            emailBody.Append("Xem tệp đính kèm để xem thông tin thực tập kỳ này");
            mail.IsBodyHtml = true;
            mail.Body = emailBody.ToString();
            using (var semesterRepository = new SemesterRepository())
            using (var companyRepository = new CompanyRepository())
            {
                var semester = semesterRepository.GetLatest().SemesterId;
                var path = $"{filePath}\\{semester}";
                var fileNames = Directory.GetFiles(path).Select(f => Path.GetFileNameWithoutExtension(f));
                var companies = companyRepository.GetAll().Where(t => fileNames.Contains(t.CompanyName));
                foreach (var cp in companies)
                {
                    mail.Attachments.Add(new Attachment($"{path}\\{cp.CompanyName}.csv"));
                    mail.To.Add(new MailAddress(cp.Email));
                    EmailSender.Send(mail);
                    mail.Attachments.Clear();
                    mail.To.Clear();
                }
            }
        }

        public void SendEmailToTeacher()
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(ConfigurationManager.AppSettings["mailAccount"]);
            StringBuilder emailBody = new StringBuilder();
            emailBody.AppendLine("Trường đại học Bách Khoa Hà Nội gửi danh sách sinh viên thực tập");
            emailBody.Append(@"<p>Nhấn vào liên kết hoặc xem tệp đính kèm để xem thông tin thực tập kỳ này: <a href = '" + @"http://sim.hust.edu.vn/Teacher/Internship" + "'> thực tập </a></p>");
            mail.IsBodyHtml = true;
            mail.Body = emailBody.ToString();
            using (var semesterRepository = new SemesterRepository())
            using (var teacherRepository = new TeacherRepository())
            using (var userRepository = new UserRepository())
            {
                var semester = semesterRepository.GetLatest().SemesterId;
                var path = $"{filePath}\\{semester}";
                var fileNames = Directory.GetFiles(path).Select(f => Path.GetFileNameWithoutExtension(f));
                mail.Subject = $"Thông tin phân công hướng dẫn thực tập kỳ {semester}";
                var teachers = teacherRepository.GetAll().Where(t => fileNames.Contains(t.TeacherId));
                foreach (var tc in teachers)
                {
                    mail.Attachments.Add(new Attachment($"{path}\\{tc.TeacherId}.csv"));
                    mail.To.Add(new MailAddress(userRepository.GetEmail(tc.TeacherId)));
                    EmailSender.Send(mail);
                    mail.Attachments.Clear();
                    mail.To.Clear();
                }
            }
        }

        public void SendEmailToStudent()
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(ConfigurationManager.AppSettings["mailAccount"]);
            StringBuilder emailBody = new StringBuilder();
            emailBody.AppendLine("Trường đại học Bách Khoa Hà Nội gửi kết quả đăng ký thực tâp");
            emailBody.Append(@"<p>Nhấn vào liên kết để xem thông tin thực tập kỳ này: <a href = '" + @"http://sim.hust.edu.vn/Student/Internship" + "'> thực tập </a></p>");
            mail.IsBodyHtml = true;
            mail.Body = emailBody.ToString();
            var students = GetByLatestSemester().Select(i => i.Student);
            using (var semesterRepository = new SemesterRepository())
            using (var userRepository=new UserRepository())
            {
                var semester = semesterRepository.GetLatest().SemesterId;
                mail.Subject = $"Thông tin phân công hướng dẫn thực tập kỳ {semester}";
                foreach (var st in students)
                {
                    mail.To.Add(new MailAddress(userRepository.GetEmail(st.StudentId)));
                } 
            }

            EmailSender.Send(mail);

        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
