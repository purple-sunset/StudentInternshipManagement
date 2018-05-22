using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Models
{
    public class Internship
    {
        [DisplayName("Mã thực tập")]
        public int InternshipId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Ngày đăng ký")]
        public DateTime RegistrationDate { get; set; }

        [Required]
        [DisplayName("Trạng thái")]
        public InternshipStatus Status { get; set; }

        [DisplayName("Sinh viên")]
        [UIHint("StudentTemplate")]
        public string StudentId { get; set; }

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
