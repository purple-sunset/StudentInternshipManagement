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

            base.Seed(context);
        }
    }
}
