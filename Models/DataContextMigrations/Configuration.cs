namespace Models.DataContextMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.WebContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContextMigrations";
        }

        protected override void Seed(Models.WebContext context)
        {
            var department = new Department()
            {
                DepartmentName = "CNTT"
            };

            context.Departments.Add(department);

            var studenClass = new StudentClass()
            {
                ClassName = "CNTT 2.04",
                DepartmentId = department.DepartmentId
            };

            context.StudentClasses.Add(studenClass);

            var student = new Student()
            {
                StudentId = "20131070",
                StudentName = "Tran Van Duc",
                Avatar = "20131070.png",
                Address = "MK",
                BirthDate = new DateTime(1995, 5, 14),
                Phone = "0123456789",
                Cpa = 3.0f,
                ClassId = studenClass.ClassId
            };

            context.Students.Add(student);

            var admin = new Admin()
            {
                AdminId = "Admin",
                AdminName = "Admin 1",
                Avatar = "avatar.png",
                Address = "MK",
                BirthDate = new DateTime(1995, 5, 14),
                Phone = "0123456789",
                DepartmentId = department.DepartmentId,
            };

            context.Admins.Add(admin);

            var teacher = new Teacher()
            {
                TeacherId = "Trung",
                TeacherName = "Nguyen Van Trung",
                Avatar = "avatar.png",
                Address = "MK",
                BirthDate = new DateTime(1995, 5, 14),
                Phone = "0123456789",
                DepartmentId = department.DepartmentId,
            };

            context.Teachers.Add(teacher);

            context.SaveChanges();
        }
    }
}
