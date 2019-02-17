using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace StudentInternshipManagement.Models.Entities
{
    public class Student : BaseEntity
    {
        [Required]
        [Range(0, 4)]
        [DisplayName("Điểm CPA")]
        public float Cpa { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Chương trình đào tạo")]
        public string Program { get; set; }

        [DisplayName("Lớp học")]
        [UIHint("StudentClassTemplate")]
        public int ClassId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual StudentClass Class { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<LearningClassStudent> LearningClassStudents { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Message> Messages { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        [InverseProperty("Members")]
        public virtual ICollection<Group> Groups { get; set; }

        [DisplayName("Mã sinh viên")]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}