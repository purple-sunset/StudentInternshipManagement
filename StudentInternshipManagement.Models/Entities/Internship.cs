using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using StudentInternshipManagement.Models.Constants;

namespace StudentInternshipManagement.Models.Entities
{
    public class Internship : BaseEntity
    {
        [DisplayName("Mã thực tập")]
        public override int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Ngày đăng ký")]
        public DateTime RegistrationDate { get; set; }

        [Required]
        [DisplayName("Trạng thái")]
        public InternshipStatus Status { get; set; }

        [DisplayName("Sinh viên")]
        [UIHint("StudentTemplate")]
        public int StudentId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Student Student { get; set; }

        [DisplayName("Lớp học")]
        [UIHint("LearningClassTemplate")]
        public int ClassId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual LearningClass Class { get; set; }

        [DisplayName("Công ty")]
        [UIHint("CompanyTemplate")]
        public int CompanyId { get; set; }

        [DisplayName("Định hướng")]
        [UIHint("TrainingMajorTemplate")]
        public int TrainingMajorId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual CompanyTrainingMajor Major { get; set; }
    }
}