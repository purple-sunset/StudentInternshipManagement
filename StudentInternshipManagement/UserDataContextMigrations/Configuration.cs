using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentInternshipManagement.Models;

namespace StudentInternshipManagement.UserDataContextMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentInternshipManagement.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"UserDataContextMigrations";
        }

        protected override void Seed(StudentInternshipManagement.Models.ApplicationDbContext context)
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            rolemanager.Create(new IdentityRole("Admin"));
            rolemanager.Create(new IdentityRole("Teacher"));
            rolemanager.Create(new IdentityRole("Student"));

            var user = new ApplicationUser
            {
                UserName = "20131070",
                Email = "20131070@student.hust.edu.vn"
            };
            var result = usermanager.Create(user, "Ab=123456789");

            usermanager.AddToRole(usermanager.FindByName("20131070").Id, "Student");

            var user2 = new ApplicationUser
            {
                UserName = "Admin",
                Email = "Admin@student.hust.edu.vn"
            };
            result = usermanager.Create(user2, "Ab=123456789");

            usermanager.AddToRole(usermanager.FindByName("Admin").Id, "Admin");

            var user3 = new ApplicationUser
            {
                UserName = "Trung",
                Email = "Trung@student.hust.edu.vn"
            };
            result = usermanager.Create(user3, "Ab=123456789");

            usermanager.AddToRole(usermanager.FindByName("Admin").Id, "Teacher");
        }
    }
}
