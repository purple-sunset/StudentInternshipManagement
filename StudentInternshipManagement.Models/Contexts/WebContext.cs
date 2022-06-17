﻿using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentInternshipManagement.Models.Entities;

namespace StudentInternshipManagement.Models.Contexts
{
    public class WebContext : IdentityDbContext<ApplicationUser>
    {
        public WebContext() : base("name=StudentInternshipManagement")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        }

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