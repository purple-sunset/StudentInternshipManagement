using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Models;
using Services;
using StudentInternshipManagement.Models;

namespace StudentInternshipManagement.Controllers
{
    public class HomeController : Controller
    {
        private static bool _isInit = false;
        public ActionResult Index()
        {
            if (_isInit)
            {
                var usermanager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
                rolemanager.Create(new IdentityRole("Student"));

                var user = new ApplicationUser
                {
                    UserName = "20131070",
                    Email = "20131070@student.hust.edu.vn",
                    Notifications = new List<Notification>()
                };
                var result = usermanager.Create(user, "Ab=123456789");

                usermanager.AddToRole(usermanager.FindByName("20131070").Id, "Student");

                var department = new Department()
                {
                    DepartmentName = "CNTT"
                };

                var c = new DepartmentService().Add(department);

                var studenClass = new StudentClass()
                {
                    ClassName = "CNTT 2.04",
                    DepartmentId = department.DepartmentId
                };

                c = new StudentClassService().Add(studenClass);

                var student = new Student()
                {
                    StudentId = "20131070",
                    StudentName = "Tran Van Duc",
                    Avatar = "20131070.png",
                    Address = "MK",
                    BirthDate = new DateTime(1995, 5, 14),
                    ClassId = studenClass.ClassId,
                    Cpa = 3.0f,
                    Phone = "0123456789"
                };

                c = new StudentService().Add(student);

                _isInit = false;
            }
            return View();
        }


    }
}