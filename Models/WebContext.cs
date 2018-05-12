using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WebContext : DbContext
    {
        public WebContext():base("name=StudentInternshipManagement")
        {
            Database.SetInitializer<WebContext>(new DataInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LearningClass>()
                .HasRequired(c => c.Subject)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LearningClass>()
                .HasRequired(c => c.Semester)
                .WithMany()
                .WillCascadeOnDelete(false);

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Semester> Semesters { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<StudentClass> StudentClasses { get; set; }

        public DbSet<LearningClass> LearningClasses { get; set; }

        public DbSet<TrainingMajor> TrainingMajors { get; set; }

        public DbSet<CompanyTrainingMajor> CompanyTrainingMajors { get; set; }

        public DbSet<Internship> Internships { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<News> Newses { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Statistic> Statistics { get; set; }
    }
}
