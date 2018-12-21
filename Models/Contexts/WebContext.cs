using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Entities;

namespace Models.Contexts
{
    public class WebContext : IdentityDbContext<ApplicationUser>
    {
        public WebContext()
        {
            Database.SetInitializer(new DataInitializer());
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //    modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

        //}

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Semester> Semesters { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<StudentClass> StudentClasses { get; set; }

        public DbSet<LearningClass> LearningClasses { get; set; }

        public DbSet<LearningClassStudent> LearningClassStudents { get; set; }

        public DbSet<TrainingMajor> TrainingMajors { get; set; }

        public DbSet<CompanyTrainingMajor> CompanyTrainingMajors { get; set; }

        public DbSet<Internship> Internships { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<News> Newses { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Statistic> Statistics { get; set; }

        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        public DbSet<EmailHistory> EmailHistories { get; set; }
    }
}
