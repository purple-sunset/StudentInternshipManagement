namespace Models.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.String(nullable: false, maxLength: 128),
                        AdminName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        Address = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 15),
                        Avatar = c.String(nullable: false, maxLength: 50),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdminId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 50),
                        CompanyDescription = c.String(nullable: false, maxLength: 100),
                        Address = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.CompanyTrainingMajors",
                c => new
                    {
                        CompanyId = c.Int(nullable: false),
                        TrainingMajorId = c.Int(nullable: false),
                        TotalTraineeCount = c.Int(nullable: false),
                        AvailableTraineeCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CompanyId, t.TrainingMajorId })
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.TrainingMajors", t => t.TrainingMajorId)
                .Index(t => t.CompanyId)
                .Index(t => t.TrainingMajorId);
            
            CreateTable(
                "dbo.TrainingMajors",
                c => new
                    {
                        TrainingMajorId = c.Int(nullable: false, identity: true),
                        TrainingMajorName = c.String(nullable: false, maxLength: 50),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrainingMajorId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectId = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(nullable: false, maxLength: 50),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubjectId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        CompanyId = c.Int(nullable: false),
                        TrainingMajorId = c.Int(nullable: false),
                        ClassId = c.Int(nullable: false),
                        LeaderId = c.String(maxLength: 128),
                        TeacherId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .ForeignKey("dbo.LearningClasses", t => t.ClassId)
                .ForeignKey("dbo.Students", t => t.LeaderId)
                .ForeignKey("dbo.CompanyTrainingMajors", t => new { t.CompanyId, t.TrainingMajorId })
                .Index(t => new { t.CompanyId, t.TrainingMajorId })
                .Index(t => t.ClassId)
                .Index(t => t.LeaderId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.LearningClasses",
                c => new
                    {
                        ClassId = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false, maxLength: 50),
                        SubjectId = c.Int(nullable: false),
                        SemesterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClassId)
                .ForeignKey("dbo.Semesters", t => t.SemesterId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.SubjectId)
                .Index(t => t.SemesterId);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        SemesterId = c.Int(nullable: false, identity: true),
                        SemesterName = c.String(nullable: false, maxLength: 10),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SemesterId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 128),
                        StudentName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        Address = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 15),
                        Avatar = c.String(nullable: false, maxLength: 50),
                        Cpa = c.Single(nullable: false),
                        ClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.StudentClasses", t => t.ClassId)
                .Index(t => t.ClassId);
            
            CreateTable(
                "dbo.StudentClasses",
                c => new
                    {
                        ClassId = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false, maxLength: 50),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClassId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 500),
                        File = c.String(nullable: false, maxLength: 50),
                        Time = c.DateTime(nullable: false),
                        StudentId = c.String(maxLength: 128),
                        TeacherId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .Index(t => t.StudentId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.String(nullable: false, maxLength: 128),
                        TeacherName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        Address = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 15),
                        Avatar = c.String(nullable: false, maxLength: 50),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Internships",
                c => new
                    {
                        InternshipId = c.Int(nullable: false, identity: true),
                        RegistrationDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        StudentId = c.String(maxLength: 128),
                        ClassId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        TrainingMajorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InternshipId)
                .ForeignKey("dbo.LearningClasses", t => t.ClassId)
                .ForeignKey("dbo.CompanyTrainingMajors", t => new { t.CompanyId, t.TrainingMajorId })
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.ClassId)
                .Index(t => new { t.CompanyId, t.TrainingMajorId });
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Content = c.String(nullable: false, maxLength: 500),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NewsId);

            CreateTable(
                    "dbo.Notifications",
                    c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 100),
                        Url = c.String(nullable: false, maxLength: 50),
                        Time = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.NotificationId)
                .Index(t => t.ApplicationUser_Id);

            AddForeignKey("dbo.Notifications", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");

            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        StatisticId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.StatisticId);
            
            CreateTable(
                "dbo.StudentGroups",
                c => new
                    {
                        Student_StudentId = c.String(nullable: false, maxLength: 128),
                        Group_GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_StudentId, t.Group_GroupId })
                .ForeignKey("dbo.Students", t => t.Student_StudentId)
                .ForeignKey("dbo.Groups", t => t.Group_GroupId)
                .Index(t => t.Student_StudentId)
                .Index(t => t.Group_GroupId);
            
            CreateTable(
                "dbo.StudentLearningClasses",
                c => new
                    {
                        Student_StudentId = c.String(nullable: false, maxLength: 128),
                        LearningClass_ClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_StudentId, t.LearningClass_ClassId })
                .ForeignKey("dbo.Students", t => t.Student_StudentId)
                .ForeignKey("dbo.LearningClasses", t => t.LearningClass_ClassId)
                .Index(t => t.Student_StudentId)
                .Index(t => t.LearningClass_ClassId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Internships", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Internships", new[] { "CompanyId", "TrainingMajorId" }, "dbo.CompanyTrainingMajors");
            DropForeignKey("dbo.Internships", "ClassId", "dbo.LearningClasses");
            DropForeignKey("dbo.Groups", new[] { "CompanyId", "TrainingMajorId" }, "dbo.CompanyTrainingMajors");
            DropForeignKey("dbo.Groups", "LeaderId", "dbo.Students");
            DropForeignKey("dbo.Groups", "ClassId", "dbo.LearningClasses");
            DropForeignKey("dbo.LearningClasses", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Messages", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Groups", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Messages", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentLearningClasses", "LearningClass_ClassId", "dbo.LearningClasses");
            DropForeignKey("dbo.StudentLearningClasses", "Student_StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentGroups", "Group_GroupId", "dbo.Groups");
            DropForeignKey("dbo.StudentGroups", "Student_StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "ClassId", "dbo.StudentClasses");
            DropForeignKey("dbo.StudentClasses", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.LearningClasses", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.TrainingMajors", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.CompanyTrainingMajors", "TrainingMajorId", "dbo.TrainingMajors");
            DropForeignKey("dbo.CompanyTrainingMajors", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Admins", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Notifications", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.StudentLearningClasses", new[] { "LearningClass_ClassId" });
            DropIndex("dbo.StudentLearningClasses", new[] { "Student_StudentId" });
            DropIndex("dbo.StudentGroups", new[] { "Group_GroupId" });
            DropIndex("dbo.StudentGroups", new[] { "Student_StudentId" });
            DropIndex("dbo.Internships", new[] { "CompanyId", "TrainingMajorId" });
            DropIndex("dbo.Internships", new[] { "ClassId" });
            DropIndex("dbo.Internships", new[] { "StudentId" });
            DropIndex("dbo.Teachers", new[] { "DepartmentId" });
            DropIndex("dbo.Messages", new[] { "TeacherId" });
            DropIndex("dbo.Messages", new[] { "StudentId" });
            DropIndex("dbo.StudentClasses", new[] { "DepartmentId" });
            DropIndex("dbo.Students", new[] { "ClassId" });
            DropIndex("dbo.LearningClasses", new[] { "SemesterId" });
            DropIndex("dbo.LearningClasses", new[] { "SubjectId" });
            DropIndex("dbo.Groups", new[] { "TeacherId" });
            DropIndex("dbo.Groups", new[] { "LeaderId" });
            DropIndex("dbo.Groups", new[] { "ClassId" });
            DropIndex("dbo.Groups", new[] { "CompanyId", "TrainingMajorId" });
            DropIndex("dbo.Subjects", new[] { "DepartmentId" });
            DropIndex("dbo.TrainingMajors", new[] { "SubjectId" });
            DropIndex("dbo.CompanyTrainingMajors", new[] { "TrainingMajorId" });
            DropIndex("dbo.CompanyTrainingMajors", new[] { "CompanyId" });
            DropIndex("dbo.Admins", new[] { "DepartmentId" });
            DropIndex("dbo.Notifications", new[] { "ApplicationUser_Id" });
            DropTable("dbo.StudentLearningClasses");
            DropTable("dbo.StudentGroups");
            DropTable("dbo.Statistics");
            DropTable("dbo.Notifications");
            DropTable("dbo.News");
            DropTable("dbo.Internships");
            DropTable("dbo.Teachers");
            DropTable("dbo.Messages");
            DropTable("dbo.StudentClasses");
            DropTable("dbo.Students");
            DropTable("dbo.Semesters");
            DropTable("dbo.LearningClasses");
            DropTable("dbo.Groups");
            DropTable("dbo.Subjects");
            DropTable("dbo.TrainingMajors");
            DropTable("dbo.CompanyTrainingMajors");
            DropTable("dbo.Companies");
            DropTable("dbo.Departments");
            DropTable("dbo.Admins");
        }
    }
}
