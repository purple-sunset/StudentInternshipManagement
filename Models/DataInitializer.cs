using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Models
{
    public class DataInitializer: DropCreateDatabaseIfModelChanges<WebContext>
    {
        protected override void Seed(WebContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("Teacher"));
            roleManager.Create(new IdentityRole("Student"));

            var user = new ApplicationUser
            {
                UserName = "20131070",
                Email = "20131070@student.hust.edu.vn"
            };
            userManager.Create(user, "Ab=123456789");

            userManager.AddToRole(user.Id, "Student");

            var user2 = new ApplicationUser
            {
                UserName = "Trung",
                Email = "Trung@student.hust.edu.vn"
            };

            userManager.Create(user2, "Ab=123456789");

            userManager.AddToRole(user2.Id, "Teacher");

            var user3 = new ApplicationUser
            {
                UserName = "Admin",
                Email = "Admin@student.hust.edu.vn"
            };

            userManager.Create(user3, "Ab=123456789");
            userManager.AddToRole(user3.Id, "Admin");

            var semesters = new List<Semester>
            {
                new Semester()
                {
                    SemesterId = 20171,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now
                }
            };

            foreach (var item in semesters)
            {
                context.Semesters.Add(item);
            }

            var departments = new List<Department>
            {
                new Department()
                {
                    DepartmentName = "CNTT"
                }
            };

            foreach (var item in departments)
            {
                context.Departments.Add(item);
            }

            var studenClasses = new List<StudentClass>
            {
                new StudentClass()
                {
                    ClassName = "CNTT 2.04",
                    Department = departments[0]
                }
            };

            foreach (var item in studenClasses)
            {
                context.StudentClasses.Add(item);
            }

            var students = new List<Student>
            {
                new Student()
                {
                    StudentId = "20131070",
                    StudentName = "Tran Van Duc",
                    Avatar = "20131070.png",
                    Address = "MK",
                    BirthDate = new DateTime(1995, 5, 14),
                    Phone = "0123456789",
                    Cpa = 3.0f,
                    Class = studenClasses[0]
                }
            };

            foreach (var item in students)
            {
                context.Students.Add(item);
            }

            var admins = new List<Admin>
            {
                new Admin()
                {
                    AdminId = "Admin",
                    AdminName = "Admin 1",
                    Avatar = "avatar.png",
                    Address = "MK",
                    BirthDate = new DateTime(1995, 5, 14),
                    Phone = "0123456789",
                    Department = departments[0]
                }
            };

            foreach (var item in admins)
            {
                context.Admins.Add(item);
            }

            var teachers = new List<Teacher>
            {
                new Teacher()
                {
                    TeacherId = "Trung",
                    TeacherName = "Nguyen Van Trung",
                    Avatar = "avatar.png",
                    Address = "MK",
                    BirthDate = new DateTime(1995, 5, 14),
                    Phone = "0123456789",
                    Department = departments[0]
                }
            };

            foreach (var item in teachers)
            {
                context.Teachers.Add(item);
            }

            var subjects = new List<Subject>
            {
                new Subject()
                {
                    SubjectId = "IT1110",
                    SubjectName = "Nhap mon CNTT",
                    Department = departments[0]
                }
            };

            foreach (var item in subjects)
            {
                context.Subjects.Add(item);
            }

            var learningClasses = new List<LearningClass>
            {
                new LearningClass()
                {
                    ClassName="IT1110 - 1",
                    Subject = subjects[0],
                    Semester = semesters[0]
                }
            };

            foreach (var item in learningClasses)
            {
                context.LearningClasses.Add(item);
            }

            var learningClassStudents=new List<LearningClassStudent>
            {
                new LearningClassStudent
                {
                    Class = learningClasses[0],
                    Student = students[0]
                }
            };

            foreach (var item in learningClassStudents)
            {
                context.LearningClassStudents.Add(item);
            }

            var majors = new List<TrainingMajor>
            {
                new TrainingMajor()
                {
                    TrainingMajorName = "C#",
                    Subject = subjects[0]
                },
                new TrainingMajor()
                {
                    TrainingMajorName = "Java",
                    Subject = subjects[0]
                }
            };

            foreach (var item in majors)
            {
                context.TrainingMajors.Add(item);
            }

            var companies = new List<Company>
            {
                new Company()
                {
                    CompanyName = "FPT",
                    CompanyDescription = "FPT",
                    Address = "17 Duy tan",
                    Email = "fpt@fpt.com.vn",
                    Phone = "0123456789"
                },
                new Company()
                {
                    CompanyName = "MISA",
                    CompanyDescription = "MISA",
                    Address = "Pham Hung",
                    Email = "misa@gmail.com",
                    Phone = "0123456789"
                }
            };

            foreach (var item in companies)
            {
                context.Companies.Add(item);
            }

            var companyMajors = new List<CompanyTrainingMajor>
            {
                new CompanyTrainingMajor()
                {
                    Company = companies[0],
                    TrainingMajor = majors[0],
                    AvailableTraineeCount = 1000,
                    TotalTraineeCount = 1000
                },
                new CompanyTrainingMajor()
                {
                    Company = companies[0],
                    TrainingMajor = majors[1],
                    AvailableTraineeCount = 500,
                    TotalTraineeCount = 500
                },
                new CompanyTrainingMajor()
                {
                    Company = companies[1],
                    TrainingMajor = majors[0],
                    AvailableTraineeCount = 700,
                    TotalTraineeCount = 700
                }
            };

            foreach (var item in companyMajors)
            {
                context.CompanyTrainingMajors.Add(item);
            }

            var internships = new List<Internship>
            {
                new Internship()
                {
                    RegistrationDate=DateTime.Now,
                    Status=InternshipStatus.Registered,
                    Student=students[0],
                    Class=learningClasses[0],
                    Major=companyMajors[0]
                }
            };

            foreach (var item in internships)
            {
                context.Internships.Add(item);
            }


            var news = new List<News>
            {
                new News()
                {
                    Title = "Thông báo về việc đăng ký đồ án tốt nghiệp kỳ hè 20173",
                    Content = "Viện CNTT&TT thông báo về việc đăng ký đồ án tốt nghiệp (ĐATN) kỳ hè 20173 như sau: Sinh viên tất cả các hệ đào tạo (bao gồm cả CNCN) chỉ có thể đăng ký làm ĐATN trong kỳ hè với …",
                    Time = new DateTime(2018,5,18)
                },
                new News()
                {
                    Title = "Chương trình thực tập tại Trung tâm SVMC – Samsung Electronic Việt Nam",
                    Content = "I. SVMC Internship Program là gì? Trong khuôn khổ hợp tác giữa Samsung Electronics Việt Nam và các trường Đại học/ Học viện lớn tại miền Bắc, SVMC Internship Program là chương trình thiết thực và ý nghĩa nhằm chuẩn …",
                    Time = new DateTime(2018,5,9)
                },
                new News()
                {
                    Title = "Thông báo hoàn thiện giấy tờ TTDN 20172",
                    Content = "Để kết thúc thực tập 20172, các bạn sinh viên lưu ý liên lạc sớm với giao viên phụ trách để phối hợp hoàn thiện giấy tờ. 1 – Bảng đánh giá sinh viên thực tập do công ty thực …",
                    Time = new DateTime(2018,5,7)
                },
                new News()
                {
                    Title = "Kế hoạch bảo vệ Đồ án tốt nghiệp học kỳ 20172",
                    Content = "Viện CNTT&TT xin thông báo về kế hoạch bảo vệ Đồ án tốt nghiệp (ĐATN) của học kỳ 20172 như sau: 1. Về cách thức tổ chức: – Bộ môn chuyên môn: Tổ chức thu quyển, phản biện, và chấm …",
                    Time = new DateTime(2018,5,2)
                }
                ,
                new News()
                {
                    Title = "Thông báo số 1 – thực tập doanh nghiệp học kỳ hè 20173",
                    Content = "Các bạn sinh viên tham khảo qui trình Thực tập doanh nghiệp để nắm rõ các thủ tục cần thực hiện. Sinh viên đăng ký nơi thực tập theo mẫu tại đây. Thời hạn hoàn thành đăng ký: 20/05/2018 Thông tin các công …",
                    Time = new DateTime(2018,4,27)
                },
                new News()
                {
                    Title = "Kế hoạch bảo vệ luận văn cao học đợt 21/4/2018",
                    Content = "B.1 Các công việc đăng ký bảo vệ: STT Công việc cần thực hiện Thời hạn hoàn thành 1.1 Học viên thống nhất với GVHD nội dung chi tiết của luận văn, tên đề tài và nộp đăng ký bảo …",
                    Time = new DateTime(2018,4,6)
                },
                new News()
                {
                    Title = "Khóa học lập trình robot hợp tác với Viện Công nghệ Shibaura",
                    Content = "Trong khuôn khổ các hoạt động hợp tác giữa ĐHBK Hà Nội và Viện Công nghệ Shibaura, Nhật Bản, Viện CNTT&TT sẽ tổ chức khóa học lập trình robot từ 1/3/2018-13/3/2018. Tham gia lớp học sẽ có 27 sinh viên …",
                    Time = new DateTime(2018,4,2)
                }
            };

            foreach (var item in news)
            {
                context.Newses.Add(item);
            }

            base.Seed(context);
        }
    }
}
